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

namespace Project_Lily.LogInUI
{
    /// <summary>
    /// Interaction logic for FindPassword.xaml
    /// </summary>
    public partial class FindPassword : UserControl
    {
        private MainWindow _mainWindow;


        public FindPassword(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Navigate(new Login(_mainWindow));
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text;
            string userId = IdBox.Text;
            DateTime? birthDate = BirthPicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(userId) || birthDate == null)
            {
                ResultTextBlock.Text = "모든 정보를 입력해주세요.";
                return;
            }

            // 여기에 비밀번호 찾기 로직을 넣으세요
            ResultTextBlock.Text = "입력하신 정보로 임시 비밀번호를 발송했습니다.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string userId = IdBox.Text.Trim();
            string birth = BirthPicker.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(birth))
            {
                ResultTextBlock.Text = "모든 정보를 입력해 주세요.";
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=UserDatabase.db;"))
            {
                conn.Open();
                string query = "SELECT Password FROM Users WHERE Name = @Name AND UserId = @UserId AND Birth = @Birth";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Birth", birth);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string password = reader.GetString(0);  // 실제로는 비밀번호 해시를 사용해야 하지만 여기선 예시로 원문 반환

                            ResultTextBlock.Text = $"비밀번호는: {password}";

                            //MessageBox.Show($"비밀번호는: {password}");
                        }
                        else
                        {
                            ResultTextBlock.Text = "입력한 정보와 일치하는 사용자가 없습니다.";
                        }
                    }
                }
            }
        }
    }
}
