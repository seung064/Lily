using Project_Lily.LogInUI;
using Project_Lily.Models;
using Project_Lily.View;
using Project_Lily.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        UnityConnet.ServerStart(); // 서버 시작

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
        _bgmPlayer.Open(new Uri(@"C:\\Users\\김철수\\source\\repos\\seung064\\Lily\\Project_Lily\\Assets\\LoginBGM.mp3", UriKind.Relative));
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

    private ProductionViewModel planetVM = new ProductionViewModel();

    public void choics(int i)
    {
        switch (i)
        {
            case 1:
                this.Navigate(new UserControl1(planetVM));// lily
                break;
            case 2:
                this.Navigate(new UserControl2(planetVM));// 테라노스 흑
                break;
            case 3:
                this.Navigate(new UserControl3(planetVM));// 아쿠아리스 물
                break;
            case 4:
                this.Navigate(new UserControl4(planetVM));// 실피 바람
                break;
            case 5:
                this.Navigate(new UserControl5(planetVM));// 인페르나 불
                break;
            case 6:
                this.Navigate(new UserControl6(planetVM));// 몬도 샤인
                break;
            default:
                MessageBox.Show("잘못된 선택입니다.");
                break;
        }
    }

    //조합 버튼 예
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        lock (UnityConnet.lockObject)
        {
            Console.WriteLine("조합 버튼 클릭");
            UnityConnet.socketData.CombinationStart = true; // 조합 시작
            UnityConnet.socketData.Status = SocketDataType.ServerDataProcessed;
        }
    }
}
