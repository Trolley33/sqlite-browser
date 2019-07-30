using System;
using Gtk;

namespace SQLiteBrowser
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();

            // Start main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Resize(400, 400);

            Application.Run();
        }

    }
}
