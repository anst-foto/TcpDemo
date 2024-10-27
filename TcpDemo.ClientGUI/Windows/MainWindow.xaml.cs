using System.Windows;
using System.Windows.Input;

namespace TcpDemo.ClientGUI.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("Executed");
    }

    private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = InputMessage.Text.Length != 0;
    }
}