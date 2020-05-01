using System;
using System.Collections.Generic;
using Model.domains;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class FindAllConcertsResponse : Response
    {
        public List<Concert> concerts { get; set; }

        public FindAllConcertsResponse(List<Concert> concerts)
        {
            this.concerts = concerts;
        }
    }
}