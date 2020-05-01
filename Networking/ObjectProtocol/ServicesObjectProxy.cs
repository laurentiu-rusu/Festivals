using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model.domains;
using Networking.ObjectProtocol;
using Persistance.repositories;
using Services.observer;
using Services.service;

namespace Networking
{
    public class ServicesObjectProxy : IService
    {
        private string host;
        private int port;

        private IObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> qresponses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        
        public ServicesObjectProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            qresponses = new Queue<Response>();
        }
        
        public void Login(string username, string password, IObserver appObserver)
        {
            InitializeConnection();
            SendRequest(new LoginRequest(username, password));
            Response response = ReadResponse();
            
            if (response is OkResponse)
            {
                this.client = appObserver;
            }

            if (response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
        }

        public void Logout(string username)
        {
            if (connection != null) {
                SendRequest(new LogoutRequest(username));
                Response response = ReadResponse();
                CloseConnection();
            }
        }

        public void UpdateConcert(int id, int takenSeats, int emptySeats)
        {
            SendRequest(new UpdateConcertRequest(id, takenSeats, emptySeats));
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
        }

        public Concert FindOneConcert(int id)
        {
            SendRequest(new FindOneConcertRequest(id));
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
            FindOneConcertResponse findDestinationByIdResponse = (FindOneConcertResponse) response;
            return findDestinationByIdResponse.concert;
        }

        public List<Concert> FindAllConcerts()
        {
            SendRequest(new FindAllConcertsRequest());
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
            FindAllConcertsResponse findAllConcertsResponse = (FindAllConcertsResponse) response;
            return findAllConcertsResponse.concerts;
        }

        public void SaveTicket(int id, int concertId, string buyerName, int seats)
        {
            SendRequest(new SaveTicketRequest(id, concertId, buyerName, seats));
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
        }

        public Ticket FindOneTicket(int id)
        {
            SendRequest(new FindOneTicketRequest(id));
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
            FindOneTicketResponse findDestinationByIdResponse = (FindOneTicketResponse) response;
            return findDestinationByIdResponse.ticket;
        }

        public List<Ticket> FindAllTickets()
        {
            SendRequest(new FindAllTicketsRequest());
            Response response = ReadResponse();
            if (response is ErrorResponse) {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new PersistanceException(errorResponse.message);
            }
            FindAllTicketsResponse findAllDestinationsResponse = (FindAllTicketsResponse) response;
            return findAllDestinationsResponse.tickets;
        }

        public int generateId()
        {
            throw new System.NotImplementedException();
        }
        
        private void HandleUpdate(Response response)
        {
            if (response is GetAllConcertsResponse){
                GetAllConcertsResponse response1 = (GetAllConcertsResponse)response;
                List<Concert> concerts = response1.concerts;
                
                try
                {
                    client.UpdateConcerts(concerts);
                }
                catch (PersistanceException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        
        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                //output.close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void SendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (qresponses)
                {
                    //Monitor.Wait(qresponses); 
                    response = qresponses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.StackTrace);
            }
            return response;
        }
		
        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }
		
        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    if (response is GetAllConcertsResponse)
                    {
                        HandleUpdate((GetAllConcertsResponse)response);
                    }
                    else
                    {
                        lock (qresponses)
                        {
                            qresponses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }
    }
}