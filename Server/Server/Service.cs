using System;
using System.Collections.Generic;
using Model.domains;
using Persistance.repositories;
using Services.observer;
using Services.service;

namespace Server.Server
{
    public class Service : IService
    {
        private IRepositoryConcert _repositoryConcert;
        private IRepositoryTicket _repositoryTicket;
        private IRepositoryUser _repositoryUser;
        
        private IDictionary<string, IObserver> connectedClients;

        public Service(IRepositoryConcert repositoryConcert, IRepositoryTicket repositoryTicket, IRepositoryUser repositoryUser)
        {
            _repositoryConcert = repositoryConcert;
            _repositoryTicket = repositoryTicket;
            _repositoryUser = repositoryUser;
            
            connectedClients = new Dictionary<string, IObserver>();
        }

        public void Login(string username, string password, IObserver appObserver)
        {
            User user = _repositoryUser.findOne(username, password);

            if (user == null)
            {
                throw new PersistanceException("User not found.");
            }
            
            connectedClients.Add(username, appObserver);
        }

        public void Logout(string username)
        {
            connectedClients.Remove(username);
        }

        public void UpdateConcert(int id, int takenSeats, int emptySeats)
        {
            _repositoryConcert.update(id, takenSeats, emptySeats);
        }

        public Concert FindOneConcert(int id)
        {
            return _repositoryConcert.findOne(id);
        }

        public List<Concert> FindAllConcerts()
        {
            List<Concert> concerts =  _repositoryConcert.findAll();
            return concerts;
        }

        public void SaveTicket(int id, int concertId, string buyerName, int seats)
        {
            int newId = generateId();
            Concert concert = FindOneConcert(concertId);
            _repositoryTicket.save(new Ticket(newId, concertId, buyerName, seats));
            int emptySeats = concert.emptySeats - seats;
            int takenSeats = concert.takenSeats + seats;

            Console.WriteLine("emptySeats: {0}: ", emptySeats);
            Console.WriteLine("takenSeats: {0}: ", takenSeats);
            
            UpdateConcert(concertId, takenSeats, emptySeats);
            
            var concerts = FindAllConcerts();
            foreach (var obs in connectedClients.Values)
            {
                obs.UpdateConcerts(concerts);
            }
        }

        public Ticket FindOneTicket(int id)
        {
            return _repositoryTicket.findOne(id);
        }

        public List<Ticket> FindAllTickets()
        {
            return _repositoryTicket.findAll();
        }

        public int generateId()
        {
            return FindAllTickets().Count + 1;
        }
    }
}