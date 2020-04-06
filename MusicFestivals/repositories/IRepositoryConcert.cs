using System.Collections.Generic;
using MusicFestivals.domains;

namespace MusicFestivals.repositories
{
    public interface IRepositoryConcert
    {
        bool update(int id, int takenSeats, int emptySeats);
        Concert findOne(int id);
        List<Concert> findAll();

    }
}