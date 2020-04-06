using System.Collections.Generic;
using MusicFestivals.domains;
using MusicFestivals.repositories;

namespace MusicFestivals.services
{
    public class ServiceConcert
    {
        private RepositoryConcert _repositoryConcert;

        public ServiceConcert(RepositoryConcert repositoryConcert)
        {
            _repositoryConcert = repositoryConcert;
        }

        public bool update(int id, int takenSeats, int emptySeats)
        {
            return _repositoryConcert.update(id, takenSeats, emptySeats);
        }

        public Concert findOne(int id)
        {
            return _repositoryConcert.findOne(id);
        }

        public List<Concert> findAll()
        {
            return _repositoryConcert.findAll();
        } 
    }
}