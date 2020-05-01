using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model.domains;
using Persistance.repositories;
using Services.observer;
using Services.service;

namespace Networking.ObjectProtocol
{
    public class ClientObjectWorker : IObserver
    {
        private IService server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        
        public ClientObjectWorker(IService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;

            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = HandleRequest((Request)request);
                    if (response != null)
                    {
                        SendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }
        
        public void UpdateConcerts(List<Concert> concerts)
        {
            try
            {
                SendResponse(new GetAllConcertsResponse(concerts));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private Response HandleRequest(Request request)
        {
            if (request is LoginRequest)
            {
                LoginRequest loginRequest = (LoginRequest) request;
                try
                {
                    server.Login(loginRequest.username, loginRequest.password, this);
                    return new OkResponse();
                }
                catch (PersistanceException e)
                {
                    return new ErrorResponse("The user does not exist.");
                }
            }

            if (request is LogoutRequest)
            {
                LogoutRequest logoutRequest = (LogoutRequest) request;

                try
                {
                    server.Logout(logoutRequest.username);
                    connected = false;
                    return new OkResponse();
                }
                catch (PersistanceException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is FindOneConcertRequest)
            {
                try {
                    Concert concert = server.FindOneConcert(((FindOneConcertRequest) request).id);
                    return new FindOneConcertResponse(concert);
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is FindOneTicketRequest)
            {
                try {
                    Ticket ticket = server.FindOneTicket(((FindOneTicketRequest) request).id);
                    return new FindOneTicketResponse(ticket);
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is FindAllConcertsRequest)
            {
                try {
                    List<Concert> concerts = server.FindAllConcerts();
                    return new FindAllConcertsResponse(concerts);
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is FindAllTicketsRequest)
            {
                try {
                    List<Ticket> tickets = server.FindAllTickets();
                    return new FindAllTicketsResponse(tickets);
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is SaveTicketRequest)
            {
                try {
                    server.SaveTicket(((SaveTicketRequest) request).id, ((SaveTicketRequest) request).idConcert, ((SaveTicketRequest) request).buyerName, ((SaveTicketRequest) request).wantedSeats);
                    return new OkResponse();
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            if (request is UpdateConcertRequest)
            {
                try {
                    server.UpdateConcert(((UpdateConcertRequest) request).id, ((UpdateConcertRequest) request).takenSeats, ((UpdateConcertRequest) request).emptySeats);
                    return new OkResponse();
                } catch (PersistanceException e) {
                    return new ErrorResponse(e.Message);
                }
            }
            
            return null;
        }

        private void SendResponse(Response response)
        {
            formatter.Serialize(stream, response);
            stream.Flush();
        }
    }
}