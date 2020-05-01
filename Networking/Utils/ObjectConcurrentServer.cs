using System.Net.Sockets;
using System.Threading;
using Networking.ObjectProtocol;
using Services.service;

namespace Networking.Utils
{
    public class ObjectConcurrentServer : AbstractConcurrentServer
    {
        private IService _service;

        public ObjectConcurrentServer(string host, int port, IService service) : base(host, port)
        {
            _service = service;
        }
        
        protected override Thread CreateWorker(TcpClient client)
        {
            ClientObjectWorker worker = new ClientObjectWorker(_service, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}