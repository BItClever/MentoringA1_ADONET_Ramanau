using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class ConnectionContext : IDisposable
    {
        private SqlConnection Connection { get; set; }
        private ConnectionStringSettings connectionString { get; set; }
        private bool disposed = false;

        public ConnectionContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
            Connection = new SqlConnection(connectionString.ConnectionString);
        }

        public SqlCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }

        public void OpenConnection()
        {
            Connection.Open();
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Connection.Close();
                }
                disposed = true;
            }
        }
        ~ConnectionContext()
        {
            Dispose(false);
        }
    }
}
