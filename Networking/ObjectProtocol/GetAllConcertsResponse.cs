using System;
using System.Collections.Generic;
using Model.domains;

namespace Networking.ObjectProtocol
{
    [Serializable]
    public class GetAllConcertsResponse : Response
    {
        public List<Concert> concerts { get; set; }

        public GetAllConcertsResponse(List<Concert> concerts)
        {
            this.concerts = concerts;
        }
    }
}