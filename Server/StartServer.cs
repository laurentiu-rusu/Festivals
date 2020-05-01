using Networking.Utils;
using Persistance.repositories;
using Server.Server;
using Services.service;

namespace Server
{
    public class StartServer
    {
        public static void Main(string[] args)
        {
            // Repositories
            RepositoryConcert repositoryConcert = new RepositoryConcert();
            RepositoryTicket repositoryTicket = new RepositoryTicket();
            RepositoryUser repositoryUser = new RepositoryUser();
            
            // Service
            IService service = new Service(repositoryConcert, repositoryTicket, repositoryUser);
            
            // Server
            AbstractServer server = new ObjectConcurrentServer("127.0.0.1", 7777, service);
            
            server.Start();
        }
    }
}