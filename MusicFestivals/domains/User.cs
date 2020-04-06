namespace MusicFestivals.domains
{
    public class User
    {
        private string username { get; set; }
        private string password { get; set; }
        
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get { return username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return password; }
            set { this.password = value; }
        }
    }
}