using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class ConnectionContext : IDisposable
    {
        public SqlConnection Connection { get; set; }
        private ConnectionStringSettings connectionString { get; set; }
        private bool disposed = false;

        public ConnectionContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
            Connection = new SqlConnection(connectionString.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Dispose(true);
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
    }
}
