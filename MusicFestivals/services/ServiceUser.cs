using MusicFestivals.domains;
using MusicFestivals.repositories;

namespace MusicFestivals.services
{
    public class ServiceUser
    {
        private RepositoryUser _repositoryUser;

        public ServiceUser(RepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public User checkLogin(string username, string password)
        {
            return _repositoryUser.findOne(username, password);
        }
    }
}