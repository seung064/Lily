using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Project_Lily.LogInUI
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    /// 
    
    public partial class AdminLogin : UserControl
    {
        private string dbPath = "Data Source=UserDatabase.db";
        private MainWindow _mainWindow;
        public AdminLogin(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            LoadUsers();
        }

        // 회원 목록 로딩
        private void LoadUsers()
        {
            List<User> users = new List<User>();

            using (SQLiteConnection conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                string query = "SELECT * FROM Users";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            Birth = reader["Birth"].ToString(),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            UserType = reader["UserType"].ToString()
                        });
                    }
                }
            }

            UserDataGrid.ItemsSource = users;
        }

        // 회원 삭제
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUsers = UserDataGrid.SelectedItems.Cast<User>().ToList();

            if (selectedUsers == null)
            {
                MessageBox.Show("삭제할 사용자를 선택하세요.");
                return;
            }

            // 관리자 자신은 삭제 못 하도록 예외 처리
            if (selectedUsers.Any(user => user.UserType == "admin"))
            {
                MessageBox.Show("관리자 계정은 삭제할 수 없습니다.");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(dbPath))
            {
                conn.Open();

                foreach (var user in selectedUsers)
                {
                    string deleteQuery = "DELETE FROM Users WHERE Id = @Id";

                    using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("사용자 삭제 완료");
            LoadUsers(); // 다시 목록 갱신
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Navigate(new Login(_mainWindow));
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResetUsersTable_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("관리자를 제외한 모든 유저 데이터를 초기화하시겠습니까?",
                                              "초기화 확인",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
                return;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=UserDatabase.db;"))
            {
                conn.Open();

                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. 현재 관리자 정보를 보관
                        var adminCmd = new SQLiteCommand("SELECT * FROM Users WHERE UserType = 'admin' LIMIT 1", conn);
                        var reader = adminCmd.ExecuteReader();

                        string name = "", username = "", userId = "", password = "", birth = "", email = "";
                        if (reader.Read())
                        {
                            name = reader["Name"].ToString();
                            username = reader["Username"].ToString();
                            userId = reader["UserId"].ToString();
                            password = reader["Password"].ToString();
                            birth = reader["Birth"].ToString();
                            email = reader["Email"].ToString();
                        }
                        reader.Close();

                        // 2. 테이블 삭제
                        new SQLiteCommand("DROP TABLE IF EXISTS Users", conn).ExecuteNonQuery();

                        // 3. 테이블 다시 생성 (Id 자동 증가 시작 포함)
                        string createTableQuery = @"
                            CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Username TEXT NOT NULL,
                            UserId TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Birth TEXT NOT NULL,
                            Email TEXT NOT NULL,
                            UserType TEXT NOT NULL
                            );";
                        new SQLiteCommand(createTableQuery, conn).ExecuteNonQuery();

                        // 4. 관리자 다시 삽입
                        string insertAdminQuery = @"
                            INSERT INTO Users (Name, Username, UserId, Password, Birth, Email, UserType)
                            VALUES (@Name, @Username, @UserId, @Password, @Birth, @Email, 'admin');";

                        var insertCmd = new SQLiteCommand(insertAdminQuery, conn);
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        insertCmd.Parameters.AddWithValue("@Username", username);
                        insertCmd.Parameters.AddWithValue("@UserId", userId);
                        insertCmd.Parameters.AddWithValue("@Password", password);
                        insertCmd.Parameters.AddWithValue("@Birth", birth);
                        insertCmd.Parameters.AddWithValue("@Email", email);
                        insertCmd.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("초기화가 완료되었습니다.");
                        LoadUsers(); // 데이터그리드 새로고침
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("초기화 실패: " + ex.Message);
                    }
                }
            }
        }
    }
}
