using System.Collections.Generic;
using MusicFestivals.domains;
using MusicFestivals.repositories;

namespace MusicFestivals.services
{
    public class ServiceTicket
    {
        private RepositoryTicket _repositoryTicket;

        public ServiceTicket(RepositoryTicket repositoryTicket)
        {
            _repositoryTicket = repositoryTicket;
        }

        public void save(string buyerName, int seats, int concertId)
        {
            _repositoryTicket.save(new Ticket(generateId(), concertId, buyerName, seats));
        }

        public Ticket findOne(int id)
        {
            return _repositoryTicket.findOne(id);
        }

        public List<Ticket> findAll()
        {
            return _repositoryTicket.findAll();
        }

        private int generateId()
        {
            int max = 0;
            List<Ticket> tickets = findAll();
            tickets.ForEach(ticket =>
            {
                if (ticket.id > max)
                {
                    max = ticket.id;
                }
            });
            return max + 1;
        }
    }
}