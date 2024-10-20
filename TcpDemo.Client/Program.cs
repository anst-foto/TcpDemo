using System.Net.Sockets;
using System.Text;

using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    await socket.ConnectAsync("127.0.0.1", 8888);
    Console.WriteLine($"Подключение к {socket.RemoteEndPoint} установлено");
    
    await using var stream = new NetworkStream(socket);
    var message = "Hello World!";
    var data = Encoding.UTF8.GetBytes(message);
    await stream.WriteAsync(data);
    
    await stream.FlushAsync();
    Console.WriteLine($"Сообщение \"{message}\" отправлено");
    
    var buffer = new byte[1024];
    var bytesRead = await stream.ReadAsync(buffer);
    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.WriteLine($"Получено сообщение \"{receivedMessage}\"");
}
catch (SocketException)
{
    Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
}