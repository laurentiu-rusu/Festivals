using System;
using System.Collections.Generic;
using Model.domains;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindAllTicketsResponse : Response
    {
        public List<Ticket> tickets { get; set; }

        public FindAllTicketsResponse(List<Ticket> tickets)
        {
            this.tickets = tickets;
        }
    }
}