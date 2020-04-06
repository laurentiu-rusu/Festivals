namespace MusicFestivals.domains
{
    public class Ticket
    {
        public int id { get; set; }
        public int idConcert { get; set; }
        public string buyerName { get; set; }
        public int wantedSeats { get; set; }

        public Ticket(int id, int idConcert, string buyerName, int wantedSeats)
        {
            this.id = id;
            this.idConcert = idConcert;
            this.buyerName = buyerName;
            this.wantedSeats = wantedSeats;
        }

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public int IdConcert
        {
            get { return idConcert; }
            set { this.idConcert = value; }
        }

        public string BuyerName
        {
            get { return buyerName; }
            set { this.buyerName = value; }
        }

        public int WantedSeats
        {
            get { return wantedSeats; }
            set { this.wantedSeats = value; }
        }
    }
}