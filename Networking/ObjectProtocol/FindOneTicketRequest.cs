using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindOneTicketRequest : Request
    {
        public int id { get; set; }

        public FindOneTicketRequest(int ticketId)
        {
            this.id = ticketId;
        }
    }
}