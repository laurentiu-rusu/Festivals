using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class LogoutRequest : Request
    {
        public string username { get; set; }

        public LogoutRequest(string username)
        {
            this.username = username;
        }
    }
}