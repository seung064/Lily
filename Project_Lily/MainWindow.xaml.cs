using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_Lily.LogInUI;

namespace Project_Lily;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

            DatabaseInitializer.CreateDatabaseAndTable(); // DB 초기화
            DatabaseInitializer.InsertAdminAccount(); // 관리자 ID 초기화

            MainContent.Content = new Login(this); // 처음에는 로그인 페이지로 시작
        }
        public void Navigate(UserControl nextControl)
        {
            MainContent.Content = nextControl;
        }
    }
