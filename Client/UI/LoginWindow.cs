using System;
using Gtk;
using Services.service;

namespace Client.UI
{
    public class LoginWindow : Window
    {
        private MainWindow mainWindow;
        
        private IService service;
        private Entry usernameEntry;
        private Entry passwordEntry;
        private Label errorLabel;
        
        public LoginWindow(string title, IService service) : base(title)
        {
            this.service = service;
            Resize(800, 600);
            SetPosition(WindowPosition.Center);
            InitUserInterface();
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void InitUserInterface()
        {
            Fixed fix = new Fixed();
            Label loginTitle = new Label("Login");
            usernameEntry = new Entry();
            passwordEntry = new Entry();
            passwordEntry.Visibility = false;
            errorLabel = new Label();
            Button loginBtn = new Button("Login");
            loginBtn.Clicked += HandleLogin;

            fix.Put(loginTitle, 374, 142);
            fix.Put(usernameEntry, 285, 236);
            fix.Put(passwordEntry, 285, 287);
            fix.Put(loginBtn, 285, 344);
            fix.Put(errorLabel, 285, 400);
            
            Add(fix);
        }

        private void HandleLogin(object sender, EventArgs e)
        {
            
            try
            {
                service.Login(usernameEntry.Text, passwordEntry.Text, mainWindow);
                usernameEntry.Text = "";
                passwordEntry.Text = "";
                errorLabel.Text = "";
                this.Hide();

                mainWindow.Init();
                mainWindow.ShowAll();
                
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Invalid username or password.";
            }
        }
    }
    
}