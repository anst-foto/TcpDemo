using TcpDemo.Lib;

var socket = TcpInit.CreateSocketServer(8888);
socket.Listen();
Console.WriteLine("Сервер запущен. Ожидание подключений...");

using var client = await socket.AcceptAsync();
Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");

var tcpMessage = new TcpMessage(client);
while (true)
{
    var message = await tcpMessage.ReadMessageAsync();
    Console.WriteLine($"Получено сообщение: {message}");

    await tcpMessage.SendMessageAsync($"Ваше сообщение: {message} получено!");
    
    if (message == "exit")
    {
        break;
    }
}

Console.WriteLine("Сервер остановлен.");