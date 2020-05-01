using System;

namespace Model.domains
{
    [Serializable]
    public class Concert
    {
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string location { get; set; }
        public int takenSeats { get; set; }
        public int emptySeats { get; set; }

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
    }
}