using System.Collections.ObjectModel;
using TcpDemo.Lib;
using WpfHelper;

namespace TcpDemo.ClientGUI.WindowModels;

public class MainWindowModel : BaseNotify
{
    private readonly TcpMessage _tcpMessage;
    
    private string? _message;
    public string? Message
    {
        get => _message; 
        set => SetField(ref _message, value);
    }
    
    public ObservableCollection<string> Messages { get; } = [];
    
    public LambdaCommand CommandSend { get; }

    public MainWindowModel()
    {
        var ip = App.Current.Resources["ServerAddress"] as string;
        var port = App.Current.Resources["ServerPort"] as int? ?? 0;
        var socket = TcpInit.CreateSocketClient(ip, port);
        _tcpMessage = new TcpMessage(socket);
        
        CommandSend = new LambdaCommand(
            execute: async o =>
            {
                await OnCommandSendAsync(o?.ToString()!);
            },
            canExecute: o => !string.IsNullOrWhiteSpace(o?.ToString()));
    }

    private async Task OnCommandSendAsync(string message)
    {
        await _tcpMessage.SendMessageAsync(message);
        
        await Task.Delay(10);
        
        var response = await _tcpMessage.ReadMessageAsync();
        Messages.Add(response);
    }
}