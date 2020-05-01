using System;
using Client.UI;
using Gtk;
using Networking;
using Services.service;

namespace Client
{
    public class StartClient
    {
        public static void Main(string[] args)
        {
            IService service = new ServicesObjectProxy("127.0.0.1", 7777);
            
            Application.Init();
            
            MainWindow mainWindow = new MainWindow("Application", service);
            LoginWindow loginWindow = new LoginWindow("Login", service);
            mainWindow.SetLoginWindow(loginWindow);
            loginWindow.SetMainWindow(mainWindow);

            loginWindow.ShowAll();
            Application.Run();
        }
    }
}