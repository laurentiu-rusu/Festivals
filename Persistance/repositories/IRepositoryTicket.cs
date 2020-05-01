using System.Collections.Generic;
using Model.domains;

namespace Persistance.repositories
{
    public interface IRepositoryTicket
    {
        void save(Ticket ticket);
        Ticket findOne(int id);
        List<Ticket> findAll();
    }
}