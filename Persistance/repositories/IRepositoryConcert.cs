using System.Collections.Generic;
using Model.domains;

namespace Persistance.repositories
{
    public interface IRepositoryConcert
    {
        bool update(int id, int takenSeats, int emptySeats);
        Concert findOne(int id);
        List<Concert> findAll();

    }
}