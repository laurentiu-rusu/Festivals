using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Model.domains;

namespace Persistance.repositories
{
    public class RepositoryConcert : IRepositoryConcert
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool update(int id, int takenSeats, int emptySeats)
        { 
            log.Info("Update concert's seats");
            IDbConnection connection = DbUtils.GetConnection();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE concerts SET takenSeats = @TAKENSEATS, emptySeats = @EMPTYSEATS WHERE id = @ID;";
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@TAKENSEATS";
            param.Value = takenSeats;
            IDbDataParameter param1 = cmd.CreateParameter();
            param1.ParameterName = "@EMPTYSEATS";
            param1.Value = emptySeats;
            IDbDataParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@ID";
            param2.Value = id;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);


            try
            {
                cmd.ExecuteReader();
                log.Info("Seats were updated");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            log.Error("Seats could not be updated");
            return false;
        }

        public Concert findOne(int id)
        {
            log.Info("Find concert by ID");
            IDbConnection connection = DbUtils.GetConnection();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM concerts WHERE id = @ID;";
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = id;
            cmd.Parameters.Add(param);
            var data = cmd.ExecuteReader();

            if (data.Read())
            {
                log.Info("Concert was found");
                return new Concert(
                    data.GetInt32(0),
                    data.GetString(1),
                    data.GetString(2),
                    data.GetString(3),
                    data.GetString(4),
                    data.GetInt32(5),
                    data.GetInt32(6)
                );
            }

            log.Info("No concert was found");
            return null;
        }

        public List<Concert> findAll()
        {
            log.Info("Find all concerts");
            List<Concert> list = new List<Concert>();
            IDbConnection connection = DbUtils.GetConnection();

            if (connection == null)
            {
                Console.WriteLine("Is null");
            }
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM concerts;";
            var data = cmd.ExecuteReader();
            
            while (data.Read())
            {
                Concert concert = new Concert(
                    data.GetInt32(0),
                    data.GetString(1),
                    data.GetString(2),
                    data.GetString(3),
                    data.GetString(4),
                    data.GetInt32(5),
                    data.GetInt32(6)
                );
                list.Add(concert);
            }
            
            log.Info("Return list of concerts");
            return list;
        }
    }
}