using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Model.domains;

namespace Persistance.repositories
{
    public class RepositoryTicket : IRepositoryTicket
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void save(Ticket ticket)
        {
            log.Info("Save new Ticket!");
            if (ticket == null)
            {
                return;
            }
            
            IDbConnection connection = DbUtils.GetConnection();
            var cmd = connection.CreateCommand();
            
            cmd.CommandText = "INSERT INTO tickets (id, idConcert, buyerName, wantedSeats) VALUES(@ID, @IDCONCERT, @BUYERNAME, @WANTEDSEATS);";
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = ticket.id;
            IDbDataParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@IDCONCERT";
            param2.Value = ticket.idConcert;
            IDbDataParameter param3 = cmd.CreateParameter();
            param3.ParameterName = "@BUYERNAME";
            param3.Value = ticket.buyerName;
            IDbDataParameter param4 = cmd.CreateParameter();
            param4.ParameterName = "@WANTEDSEATS";
            param4.Value = ticket.wantedSeats;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            
            try
            {
                cmd.ExecuteScalar();
                log.Info("Ticket added to db");
            }
            catch (Exception ex)
            {
                log.Error("Cannot save ticket to database");
                Console.WriteLine(ex);
            }
        }

        public Ticket findOne(int id)
        {
            log.Info("Find ticket by ID");
            IDbConnection connection = DbUtils.GetConnection();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE id = @ID;";
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = id;
            cmd.Parameters.Add(param);
            var data = cmd.ExecuteReader();

            if (data.Read())
            {
                log.Info("Ticket was found");
                return new Ticket(
                    data.GetInt32(0),
                    data.GetInt32(1),
                    data.GetString(2),
                    data.GetInt32(3)
                );
            }

            log.Info("No ticket was found");
            return null;
        }

        public List<Ticket> findAll()
        {
            log.Info("Find all tickets");
            List<Ticket> list = new List<Ticket>();
            IDbConnection connection = DbUtils.GetConnection();

            if (connection == null)
            {
                Console.WriteLine("Is null");
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets;";
            var data = cmd.ExecuteReader();

            while (data.Read())
            {
                list.Add(new Ticket(
                    data.GetInt32(0),
                    data.GetInt32(1),
                    data.GetString(2),
                    data.GetInt32(3)
                ));
            }


            log.Info("Return list of concerts");
            return list;
        }
    }
}