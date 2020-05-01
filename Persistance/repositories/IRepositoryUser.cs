using Model.domains;

namespace Persistance.repositories
{
    public interface IRepositoryUser
    {
        User findOne(string username, string password);
    }
}