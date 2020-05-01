using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class ErrorResponse : Response
    {
        public string message { get; set; }

        public ErrorResponse(string message)
        {
            this.message = message;
        }
    }
}