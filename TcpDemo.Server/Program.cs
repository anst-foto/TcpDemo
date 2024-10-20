using System.Net;
using System.Net.Sockets;
using System.Text;

var ipPoint = new IPEndPoint(IPAddress.Any, 8888);
using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);
socket.Listen();
Console.WriteLine("Сервер запущен. Ожидание подключений...");

using var client = await socket.AcceptAsync();
Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");

await using var stream = new NetworkStream(client);
var byteArray = new byte[1024];
var bytes = await stream.ReadAsync(byteArray);
var response = Encoding.UTF8.GetString(byteArray, 0, bytes);
Console.WriteLine(response);

var message = Encoding.UTF8.GetBytes($"Ваше сообщение: {response} получено!");
await stream.WriteAsync(message);