using System;
using Model.domains;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindOneConcertResponse : Response
    {
        public Concert concert { get; set; }

        public FindOneConcertResponse(Concert concert)
        {
            this.concert = concert;
        }
    }
}