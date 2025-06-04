using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        private MediaPlayer? _bgmPlayer;

        public MainWindow()
        {
            InitializeComponent();
            

            DatabaseInitializer.CreateDatabaseAndTable(); // DB 초기화
            DatabaseInitializer.InsertAdminAccount(); // 관리자 ID 초기화

            MainContent.Content = new Login(this); // 처음에는 로그인 페이지로 시작

            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }
        public void Navigate(UserControl nextControl)
        {
            MainContent.Content = nextControl;
        }
        private void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            _bgmPlayer = new MediaPlayer();
            _bgmPlayer.Open(new Uri("Assets/LoginBGM.mp3", UriKind.Relative));
            _bgmPlayer.Volume = 0.5;

            _bgmPlayer.MediaOpened += (s, args) =>
            {
                _bgmPlayer.Play();
            };

                _bgmPlayer.MediaEnded += (s, args) =>
            {
                _bgmPlayer.Position = TimeSpan.Zero;
                _bgmPlayer.Play();
            };
            _bgmPlayer.MediaFailed += (s, args) =>
            {
                MessageBox.Show("음악 재생 실패: " + args.ErrorException?.Message);
            };
    }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            _bgmPlayer?.Stop();
            _bgmPlayer?.Close();
            _bgmPlayer = null;
        }
    }
