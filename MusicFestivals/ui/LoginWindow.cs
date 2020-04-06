using System;
using Gtk;
using MusicFestivals.domains;
using MusicFestivals.services;

namespace MusicFestivals.ui
{
    public class LoginWindow : Window
    {
        private Window mainWindow;
        
        private ServiceUser _userService;
        private Entry usernameEntry;
        private Entry passwordEntry;
        private Label errorLabel;
        
        public LoginWindow(string title, ServiceUser userService) : base(title)
        {
            this._userService = userService;
            Resize(800, 600);
            SetPosition(WindowPosition.Center);
            InitUserInterface();
        }

        public void SetMainWindow(Window mainWindow)
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
            User user = _userService.checkLogin(usernameEntry.Text, passwordEntry.Text);

            if (user != null)
            {
                usernameEntry.Text = "";
                passwordEntry.Text = "";
                errorLabel.Text = "";
                this.Hide();
                mainWindow.ShowAll();
            }
            else
            {
                errorLabel.Text = "Invalid username or password.";
            }
        }
    }
    
}