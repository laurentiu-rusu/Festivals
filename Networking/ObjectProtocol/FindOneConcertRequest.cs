using System;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindOneConcertRequest : Request
    {
        public int id { get; set; }

        public FindOneConcertRequest(int concertId)
        {
            this.id = concertId;
        }
    }
}