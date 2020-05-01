using System;
using Model.domains;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindOneTicketResponse : Response
    {
        public Ticket ticket { get; set; }

        public FindOneTicketResponse(Ticket ticket)
        {
            this.ticket = ticket;
        }
    }
}