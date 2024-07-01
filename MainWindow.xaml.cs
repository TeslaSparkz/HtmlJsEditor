using System;
using System.Windows;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;
using System.Windows.Navigation;

namespace HtmlJsEditor
{
    public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Set the syntax highlighting
        CodeEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("HTML");
        CodeEditor.Background = Brushes.Black;
        CodeEditor.Foreground = Brushes.White;

        CodeEditor.Text = @"<!DOCTYPE html>
<html>
<head>
    <title>HTML &amp; JS Editor</title>
</head>
<body>
    <canvas id='myCanvas' width='200' height='100' style='border:1px solid #000000;'></canvas>
    <script>
        var canvas = document.getElementById('myCanvas');
        var ctx = canvas.getContext('2d');
        
        ctx.fillStyle = '#FF0000';
        ctx.fillRect(10, 10, 150, 80);
        
        function drawCircle() {
            ctx.beginPath();
            ctx.arc(95, 50, 40, 0, 2 * Math.PI);
            ctx.stroke();
        }
    </script>
</body>
</html>";

        PreviewBrowser.LoadCompleted += PreviewBrowser_LoadCompleted;
    }

    private void PreviewBrowser_LoadCompleted(object sender, NavigationEventArgs e)
    {
        // Set the object for scripting
        PreviewBrowser.ObjectForScripting = new JsConsole(this);
    }

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string htmlContent = CodeEditor.Text;
            PreviewBrowser.NavigateToString(htmlContent);
            LogToConsole("HTML content successfully loaded in the browser.");
        }
        catch (Exception ex)
        {
            LogToConsole($"Error: {ex.Message}");
        }
    }

    public void LogToConsole(string message)
    {
        ConsoleOutput.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
        ConsoleOutput.ScrollToEnd();
    }

    private void DrawCircleButton_Click(object sender, RoutedEventArgs e)
    {
        // Example of calling JavaScript function from C#
        ((JsConsole)PreviewBrowser.ObjectForScripting).DrawCircle();
    }
}

}
