using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class UpdateConcertRequest : Request
    {
        public int id { get; set; }
        public int takenSeats { get; set; }
        public int emptySeats { get; set; }

        public UpdateConcertRequest(int id, int takenSeats, int emptySeats)
        {
            this.id = id;
            this.takenSeats = takenSeats;
            this.emptySeats = emptySeats;
        }
    }
}