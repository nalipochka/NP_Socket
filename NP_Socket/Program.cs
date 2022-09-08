using System.Net;
using System.Net.Sockets;
using System.Text;




IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint endPoint = new IPEndPoint(ip, 1024);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

socket.Bind(endPoint);
socket.Listen();

Console.WriteLine("Server started");

while (true)
{
    Socket newSocket = socket.Accept();
    Console.WriteLine($"Socket {newSocket.RemoteEndPoint} connected!");
    Console.WriteLine($"New server socket with EP {newSocket.LocalEndPoint} was created!");


    int length = 0;
    byte[] buff = new byte[1024];
    StringBuilder sb = new StringBuilder();
    try
    {
        do
        {
            length = newSocket.Receive(buff);
            string str = Encoding.Default.GetString(buff, 0, length);
            sb.Append(str);
        } while (length>0);
        Console.WriteLine(sb.ToString());
        string message = $"At {DateTime.Now.ToLongTimeString()} from {socket.LocalEndPoint}, message received: \"Hello Client\"";
        buff = Encoding.Default.GetBytes(message);
        newSocket.Send(buff);
    }
    catch (SocketException ex)
    {

    }
    finally
    {
        newSocket?.Close();
    }
}



