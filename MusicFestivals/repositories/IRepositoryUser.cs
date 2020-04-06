using MusicFestivals.domains;

namespace MusicFestivals.repositories
{
    public interface IRepositoryUser
    {
        User findOne(string username, string password);
    }
}