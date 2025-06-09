using Project_Lily.ViewModels;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_Lily.Models
{
    internal class UnityConnet
    {
        public static SocketData socketData = new SocketData();
        public static readonly object lockObject = new object();

        public static async void ServerStart()
        {
            _ = SocketServerAsync(); // fire-and-forget
        }

        public static async Task SocketServerAsync()
        {
            try
            {
                TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 51111);
                listener.Start();
                Console.WriteLine("클라이언트 접속 대기 중...");
                TcpClient c = await listener.AcceptTcpClientAsync();
                Console.WriteLine("클라이언트가 접속했습니다.");
                NetworkStream stream = c.GetStream();

                // 수신 비동기 루프 (루프는 있지만 await로 블로킹 없음)
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
                        while (c.Connected)
                        {
                            if (!stream.DataAvailable)
                            {
                                await Task.Delay(100);
                                continue;
                            }

                            string json = null;
                            try
                            {
                                if (stream.DataAvailable)
                                {
                                    lock(lockObject)     
                                        json = reader.ReadString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch (EndOfStreamException)
                            {
                                Console.WriteLine("클라이언트가 연결을 종료했습니다.");
                                break;
                            }
                            catch (IOException ioEx)
                            {
                                Console.WriteLine("수신 IO 오류: " + ioEx.Message);
                                break;
                            }

                            if (json == null)
                                continue;

                            lock (lockObject)
                            {
                                socketData = SocketData.Parse(json);
                                if (!string.IsNullOrWhiteSpace(socketData.Commend))
                                {
                                    Console.WriteLine("커맨드: " + socketData.Commend);
                                    var mainWindow = App.Current.Dispatcher.Invoke(() => App.Current.MainWindow as MainWindow);
                                    mainWindow?.Dispatcher.Invoke(() =>
                                    {
                                        mainWindow.choics(int.Parse(socketData.Commend));
                                    });
                                    socketData.Commend = string.Empty; // 커맨드 처리 후 초기화
                                    socketData.Status = SocketDataType.ClientDataProcessed;
                                }
                                if (socketData.CombinationSuccess)// 조합 성공
                                {
                                    ProductionViewModel pv=new ProductionViewModel();
                                    pv.OnCombinationSuccess(); // 조합 성공 이벤트 호출
                                }
                            }
                            Console.WriteLine("수신: " + json);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("수신 오류: " + ex.Message);
                    }
                });

                // 송신 비동기 루프 (필요할 때만 전송)
                var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
                while (c.Connected)
                {
                    await Task.Delay(100);
                    string json;
                    lock (lockObject)
                    {
                        socketData.Id++; // ID 증가
                        json = socketData.ToJsonString();
                    }
                    try
                    {
                        // 연결이 살아있는지 명확히 확인
                        if (!c.Connected || !stream.CanWrite)
                        {
                            Console.WriteLine("송신 중단: 연결이 종료됨");
                            break;
                        }
                        
                        await Task.Run(() =>
                        {
                            writer.Write(json);
                            writer.Flush();
                        });
                        Console.WriteLine("송신: " + json);
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine("송신 IO 오류: " + ioEx.Message);
                        break;
                    }
                    catch (ObjectDisposedException)
                    {
                        Console.WriteLine("송신 중단: 스트림이 이미 닫힘");
                        break;
                    }
                }

                c.Close();
                listener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"서버 연결 오류 51111: {ex.Message}");
            }
        }
    }
}
