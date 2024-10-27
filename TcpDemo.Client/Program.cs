using System.Net.Sockets;
using TcpDemo.Lib;

Socket? socket = null;

try
{
    socket = await TcpInit.CreateSocketClientAsync("127.0.0.1", 8888);
    Console.WriteLine($"Подключение к {socket.RemoteEndPoint} установлено");

    var tcpMessage = new TcpMessage(socket);

    while (true)
    {
        Console.Write("Введите сообщение: ");
        var message = Console.ReadLine();
        await tcpMessage.SendMessageAsync(message);
        
        message = await tcpMessage.ReadMessageAsync();
        Console.WriteLine($"Получено сообщение {message}");
    }
}
catch (SocketException)
{
    Console.WriteLine($"Не удалось установить подключение с {socket?.RemoteEndPoint}");
}