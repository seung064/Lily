using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Win32;
using Project_Lily.LogInUI;
using System.Data.SQLite;
using System.Data.Entity;

namespace Project_Lily
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    
    public partial class Login : UserControl
    {
        private MainWindow _mainWindow;


        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            
        }

        private void CheckTables_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SQLiteConnection("Data Source=users.db;"))
            {
                conn.Open();
                var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", conn);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show("존재하는 테이블: " + reader.GetString(0));
                }
                conn.Close();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void RegisterText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 예: 회원가입 화면으로 이동
            //UserControl.Content = new SignUp(); // Register는 UserControl
            _mainWindow.Navigate(new SignUp(_mainWindow));
            //MessageBox.Show("회원가입 기능은 아직 구현되지 않았습니다.");
        }

        private void FindPwText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 예: 비밀번호 찾기 화면 이동
            _mainWindow.Navigate(new FindPassword(_mainWindow));
            //MessageBox.Show("비밀번호 찾기 기능은 아직 구현되지 않았습니다.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userId = IdTextBox.Text;
            string password = PasswordBox.Password;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=UserDatabase.db;"))
            {
                conn.Open();
                string query = "SELECT UserType FROM Users WHERE UserId = @UserId AND Password = @Password";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password); // 해시된 비밀번호라면 여기서도 해시해야 함

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userType = Convert.ToString(reader["UserType"]) ?? "user";

                            if (userType == "admin")
                            {
                                // 관리자 전용 화면 이동
                                _mainWindow.Navigate(new AdminLogin(_mainWindow));
                            }
                            else
                            {
                                // 일반 사용자 화면 이동(게임 시작)
                                _mainWindow.Navigate(new FindPassword(_mainWindow));
                            }
                        }
                        else
                        {
                            // 로그인 실패
                            MessageBox.Show("아이디 또는 비밀번호가 일치하지 않습니다");
                        }
                    }
                }
            }
        }
    }

    // Token 확인용 11 12
}
