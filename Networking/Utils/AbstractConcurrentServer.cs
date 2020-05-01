using System.Net.Sockets;
using System.Threading;

namespace Networking.Utils
{
    public abstract class AbstractConcurrentServer : AbstractServer
    {
        public AbstractConcurrentServer(string host, int port) : base(host, port)
        { }

        public override void ProcessRequest(TcpClient client)
        {

            Thread t = CreateWorker(client);
            t.Start();

        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}