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
using Project_Lily.ViewModels;

namespace Project_Lily.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //DataContext = new PageChangeViewModel();  // 뷰모델을 DataContext로 연결
    }

    private ProductionViewModel sharedVM = new ProductionViewModel();

    private void ShowUC1_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl1(sharedVM));
    }

    private void ShowUC2_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Clear();
        MainGrid.Children.Add(new UserControl2(sharedVM));
    }
}