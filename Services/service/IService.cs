using System.Collections.Generic;
using Model.domains;
using Services.observer;

namespace Services.service
{
    public interface IService
    {
        void Login(string username, string password, IObserver appObserver);
        void Logout(string username);

        void UpdateConcert(int id, int takenSeats, int emptySeats);

        Concert FindOneConcert(int id);

        List<Concert> FindAllConcerts();

        void SaveTicket(int id, int concertId, string buyerName, int seats);

        Ticket FindOneTicket(int id);

        List<Ticket> FindAllTickets();

        int generateId();
    }
}