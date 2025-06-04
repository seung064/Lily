using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace Project_Lily
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        private MainWindow _mainWindow;

        public SignUp(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Navigate(new Login(_mainWindow));
        }
        
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text;
            string username = UsernameBox.Text;
            string userId = IdBox.Text;
            string password = PasswordBox.Password;
            // DatePicker에서 선택된 날짜를 yyyy-MM-dd 형식으로 문자열로 저장
            string birth = BirthPicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "";
            string email = EmailBox.Text;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=UserDatabase.db;"))
            {
                conn.Open();

                string insertQuery = @"
                        INSERT INTO Users (Name, Username, UserId, Password, Birth, Email, UserType)
                        VALUES (@Name, @Username, @UserId, @Password, @Birth, @Email, @UserType);";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password); // 실제론 해시 권장
                    cmd.Parameters.AddWithValue("@Birth", birth);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@UserType", "user");


                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("회원가입이 완료되었습니다!");

                        // 회원가입 성공 -> 로그인 화면으로 돌아가기
                        _mainWindow.Navigate(new Login(_mainWindow));
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.Message.Contains("UNIQUE constraint failed: Users.UserId"))
                        {
                            // 이미 사용중인 아이디 확인
                            MessageBox.Show("이미 사용 중인 아이디입니다. 다른 아이디를 사용해 주세요.");
                        }

                        else
                        {
                            MessageBox.Show("회원가입 실패: " + ex.Message);
                        }
                    }
                    
                }
            }
        }

        
    }
}
