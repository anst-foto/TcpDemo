using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpDemo.Lib;

public static class TcpInit
{
    public static Socket ServerSocket { get; private set; }
    public static Socket ClientSocket { get; private set; }
    
    public static Socket CreateSocketServer(int port)
    {
        var ipPoint = new IPEndPoint(IPAddress.Any, port);
        ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ServerSocket.Bind(ipPoint);
        
        return ServerSocket;
    }

    public static async Task<Socket> CreateSocketClientAsync(string ip, int port)
    {
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        await ClientSocket.ConnectAsync(ip, port);
        
        return ClientSocket;
    }
    
    public static Socket CreateSocketClient(string ip, int port)
    {
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ClientSocket.Connect(ip, port);
        
        return ClientSocket;
    }
}

public class TcpMessage
{
    private readonly NetworkStream _stream;

    public TcpMessage(Socket socket)
    {
        _stream = new NetworkStream(socket);
    }
    
    public async Task<string> ReadMessageAsync()
    {
        var byteArray = new byte[1024];
        var bytes = await _stream.ReadAsync(byteArray);
        return Encoding.UTF8.GetString(byteArray, 0, bytes);
    }
    
    public async Task SendMessageAsync(string message)
    {
        var byteArray = Encoding.UTF8.GetBytes(message);
        await _stream.WriteAsync(byteArray);
        await _stream.FlushAsync();
    }
}