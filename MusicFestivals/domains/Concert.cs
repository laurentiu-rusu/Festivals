namespace MusicFestivals.domains
{
    public class Concert
    {
        private int id { get; set; }
        private string name { get; set; }
        private string date { get; set; }
        private string time { get; set; }
        private string location { get; set; }
        private int takenSeats { get; set; }
        private int emptySeats { get; set; }

        public Concert(int id, string name, string date, string time, string location, int takenSeats, int emptySeats)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.time = time;
            this.location = location;
            this.takenSeats = takenSeats;
            this.emptySeats = emptySeats;
        }

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public string Date
        {
            get { return date; }
            set { this.date = value; }
        }

        public string Time
        {
            get { return time; }
            set { this.time = value; }
        }

        public string Location
        {
            get { return location; }
            set { this.location = value; }
        }

        public int TakenSeats
        {
            get { return takenSeats; }
            set { this.takenSeats = value; }
        }

        public int EmptySeats
        {
            get { return emptySeats; }
            set { this.emptySeats = value; }
        }
    }
}