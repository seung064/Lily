using System.IO;
using System.Net.Sockets;
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

namespace Project_Lily;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    async Task Server()
    {
        TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 51111);
        listener.Start();
        using (TcpClient c = listener.AcceptTcpClient())
        {
            using (NetworkStream stream = c.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                MessageBox.Show("Received: " + message, "Server Message", MessageBoxButton.OK, MessageBoxImage.Information);
                // 안녕 클라이언트
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write("hello 클라이언트");
                // 종료
                stream.Close();
            }
            listener.Stop();
        }
    }
}