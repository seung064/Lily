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
    
}