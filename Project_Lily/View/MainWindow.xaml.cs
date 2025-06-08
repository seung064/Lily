using Project_Lily.Models;
using Project_Lily.ViewModels;
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

namespace Project_Lily.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();


    }

    private ProductionViewModel planetVM = new ProductionViewModel();

    private void ShowUC1_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl1(planetVM));
    }

    private void ShowUC2_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl2(planetVM));
    }

    private void ShowUC3_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl3(planetVM));
    }

    private void ShowUC4_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl4(planetVM));
    }

    private void ShowUC5_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl5(planetVM));
    }

    
    private void ShowUC6_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl6(planetVM));
    }

    public void choics(int i)
    {
        switch (i)
        {
            case 1:
                ShowUC1_Click(null, null);
                break;
            case 2:
                ShowUC2_Click(null, null);
                break;
            case 3:
                ShowUC3_Click(null, null);
                break;
            case 4:
                ShowUC4_Click(null, null);
                break;
            case 5:
                ShowUC5_Click(null, null);
                break;
            case 6:
                ShowUC6_Click(null, null);
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