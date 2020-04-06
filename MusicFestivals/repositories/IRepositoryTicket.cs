using System.Collections.Generic;
using MusicFestivals.domains;

namespace MusicFestivals.repositories
{
    public interface IRepositoryTicket
    {
        void save(Ticket ticket);
        Ticket findOne(int id);
        List<Ticket> findAll();
    }
}