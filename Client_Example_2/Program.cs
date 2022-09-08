using System.Net;
using System.Net.Sockets;
using System.Text;



IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint endPoint = new IPEndPoint(ip, 1024);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

try
{
    socket.Connect(endPoint);
    Console.WriteLine("Выберите варинт сообщения:");
    Console.WriteLine("1 --- Вернуть время на сервере");
    Console.WriteLine("2 --- Вернуть дату на сервере");
    int check = int.Parse(Console.ReadLine());
    string mess;
    byte[] buff;
    switch (check)
    {
        case 1:
            {
                mess = "time";
                buff = Encoding.Default.GetBytes(mess);
                socket.Send(buff);
                socket.Shutdown(SocketShutdown.Send);

            }
            break;
        case 2:
            {
                mess = "date";
                buff = Encoding.Default.GetBytes(mess);
                socket.Send(buff);
                socket.Shutdown(SocketShutdown.Send);
            }
            break;
        default:
            break;
    }
    buff = new byte[1024];
    int length = 0;
    
    StringBuilder sb = new StringBuilder();
    do
    {
        length = socket.Receive(buff);
        string message = Encoding.Default.GetString(buff, 0, length);
        sb.Append(message);
    } while (socket.Available >0);
    Console.WriteLine(sb.ToString());
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
    Console.ReadLine();
}