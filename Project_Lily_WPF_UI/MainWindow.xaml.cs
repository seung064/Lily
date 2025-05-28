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

namespace Project_Lily_WPF_UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        imagaeTest.Source = new BitmapImage(new Uri("Assets/Aquarius.png", UriKind.Relative));
        imagaeTest1.Source = new BitmapImage(new Uri("Assets/imageTest1.png", UriKind.Relative));
        imagaeTest2.Source = new BitmapImage(new Uri("Assets/imageTest2.png", UriKind.Relative));
        imagaeTest3.Source = new BitmapImage(new Uri("Assets/imageTest3.png", UriKind.Relative));
        imagaeTest11.Source = new BitmapImage(new Uri("Assets/imageTest1.png", UriKind.Relative));
        imagaeTest22.Source = new BitmapImage(new Uri("Assets/imageTest2.png", UriKind.Relative));
        imagaeTest33.Source = new BitmapImage(new Uri("Assets/imageTest3.png", UriKind.Relative));
    }
}