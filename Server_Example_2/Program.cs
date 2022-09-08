using System.Net;
using System.Net.Sockets;
using System.Text;



IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint endPoint = new IPEndPoint(ip, 1024);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

socket.Bind(endPoint);
socket.Listen();

while (true)
{
    Socket newS = socket.Accept();
    Console.WriteLine($"Socket {newS.RemoteEndPoint} connected");

    int length = 0;
    byte[] buffer = new byte[1024];
    string strCheck;
    StringBuilder sb = new StringBuilder();
    try
    {
        do
        {
            length = newS.Receive(buffer);
            strCheck = Encoding.Default.GetString(buffer);
            sb.Append(strCheck);
        } while (length > 0);
        if (strCheck.Contains("time"))
        {
            string message = DateTime.Now.ToLongTimeString();
            buffer = Encoding.Default.GetBytes(message);
            newS.Send(buffer);
            Console.WriteLine("Current time sent!");
        }
        else
        {
            string message = DateTime.Now.ToLongDateString();
            buffer = Encoding.Default.GetBytes(message);
            newS.Send(buffer);
            Console.WriteLine("Current date sent!"); 
        }

       
    }
    catch(SocketException ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        newS?.Close();
    }
}
