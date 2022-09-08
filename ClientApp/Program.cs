using System.Net;
using System.Net.Sockets;
using System.Text;



IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint iPEnd = new IPEndPoint(ip, 1024);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
try
{
    socket.Connect(iPEnd);
    string message = $"At {DateTime.Now.ToLongTimeString()} from {socket.LocalEndPoint}, message received: \"Hello Server\"";
    byte[] buff = Encoding.Default.GetBytes(message);
    socket.Send(buff);
    buff = new byte[1024];
    int length = 0;
    socket.Shutdown(SocketShutdown.Send);
    StringBuilder sb = new StringBuilder();
    do
    {
        length = socket.Receive(buff);
        string str =Encoding.Default.GetString(buff);
        sb.Append(str);
    } while (socket.Available > 0);
    Console.WriteLine(sb.ToString());


}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
    Console.ReadLine();
}