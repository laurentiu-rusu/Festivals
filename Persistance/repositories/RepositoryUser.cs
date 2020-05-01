using System;
using System.Data;
using log4net;
using Model.domains;

namespace Persistance.repositories
{
    public class RepositoryUser : IRepositoryUser
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public User findOne(string username, string password)
        {
            log.Info("Find user by username and password");
            IDbConnection connection = DbUtils.GetConnection();
            
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE username = @USERNAME AND password = @PASSWORD;";
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@USERNAME";
            param.Value = username;
            IDbDataParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@PASSWORD";
            param2.Value = password;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add(param2);
            var data = cmd.ExecuteReader();

            if (data.Read())
            {
                log.Info("The user was found");
                return new User(
                    data.GetString(0),
                    data.GetString(1)
                );
            }

            log.Error("The user could not be found");
            return null;
        }
    }
}