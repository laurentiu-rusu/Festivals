using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class SaveTicketRequest : Request
    {
        public int id { get; set; }
        public int idConcert { get; set; }
        public string buyerName { get; set; }
        public int wantedSeats { get; set; }

        public SaveTicketRequest(int id, int idConcert, string buyerName, int wantedSeats)
        {
            this.id = id;
            this.idConcert = idConcert;
            this.buyerName = buyerName;
            this.wantedSeats = wantedSeats;
        }
    }
}