using System;
using System.Windows;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Media;
using System.Windows.Navigation;

namespace HtmlJsEditor
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsConsole
    {
        private MainWindow _mainWindow;

        public JsConsole(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void Log(string message)
        {
            _mainWindow.Dispatcher.Invoke(() =>
            {
                _mainWindow.ConsoleOutput.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
                _mainWindow.ConsoleOutput.ScrollToEnd();
            });
        }

        public void DrawCircle()
        {
            _mainWindow.Dispatcher.Invoke(() =>
            {
                // Example of interacting with the canvas
                _mainWindow.PreviewBrowser.InvokeScript("drawCircle");
                Log("Circle drawn on canvas.");
            });
        }
    }
}
