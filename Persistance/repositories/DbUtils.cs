using System;
using System.Data;
using log4net;
using Mono.Data.Sqlite;

namespace Persistance.repositories
{
    public static class DbUtils
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string connectionString;
        private static SqliteConnection connection;

        private static string getDbProperty()
        {
            log.Info("Get DB connection string from config file.");
            string path = System.Configuration.ConfigurationManager.AppSettings.Get("jdbc.url");
            if (path == null)
            {
                return "Data Source=/Users/user/UBB/Semestrul_ll/MPP/Festivals/Music_festival.sqlite,Version=3";
            }
            log.InfoFormat("Db conn string: {0}", path);
            return path;
        }
        private static SqliteConnection GetNewConnection()
        {
            log.Info("Get a new database connection");
            SqliteConnection conn = null;

            try
            {
                connectionString = getDbProperty();
                conn = new SqliteConnection(connectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
                log.Error("Could not connect to database");
            }

            log.Info("Connection established");
            return conn;
        }

        public static IDbConnection GetConnection()
        {
            log.Info("Get database connection");
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = GetNewConnection();
            }

            log.Info("Connection returned.");
            return connection;
        }
    }
}