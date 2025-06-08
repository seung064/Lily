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

        // 이메일 확인 클래스
        bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');

            // '@'이 있어야 하고, '.'이 '@' 뒤에 있어야 하며, 위치가 유효해야 함
            return atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1;
        }

        // 비밀번호 검사 클래스
        bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true; // 특수문자 (예: !, @, # 등)
            }

            return hasUpper && hasLower && hasDigit && hasSpecial;
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

            // 1. 공란 체크
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(birth) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("모든 항목을 입력해주세요.");
                return;
            }


            // 2. 아이디 유효성 검사 (소문자+숫자만 허용)
            if (string.IsNullOrWhiteSpace(userId)
                || !userId.All(c => char.IsLower(c) || char.IsDigit(c))
                || userId.Length < 4 || userId.Length > 12)
            {
                MessageBox.Show("아이디는 4~12자의 소문자와 숫자만 사용할 수 있습니다.");
                return;
            }

            // 3. 이메일 형식 검사
            if (!IsValidEmail(email))
            {
                MessageBox.Show("유효한 이메일 형식을 입력해 주세요.");
                return;
            }

            // 4. 비밀번호 검사: 6자 이상, 숫자+영문자+특수문자 포함 권장
            if (!IsValidPassword(password))
            {
                MessageBox.Show("비밀번호는 8자 이상이며, 대문자, 소문자, 숫자, 특수문자를 모두 포함해야 합니다.");
                return;
            }

            // 5. 데이터베이스에 저장
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
