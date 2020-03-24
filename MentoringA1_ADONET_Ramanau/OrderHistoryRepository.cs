using MentoringA1_ADONET_Ramanau.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class OrderHistoryRepository
    {
        private readonly ConnectionContext context;
        public OrderHistoryRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<CustOrderHist> GetAll(string customerID)
        {
            context.OpenConnection();
            Dictionary<Product, int> custorderhist = new Dictionary<Product, int>();
            List<CustOrderHist> orderHist = new List<CustOrderHist>();

            var command = context.CreateCommand();
            command.CommandText = "CustOrderHist";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CustomerID", SqlDbType.NChar);
            command.Parameters["@CustomerID"].Value = customerID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustOrderHist or = new CustOrderHist();
                or.ProductName = reader.GetString(0);
                or.Total = reader.GetInt32(1);
                orderHist.Add(or);
            }
            context.CloseConnection();
            return orderHist;
        }
    }
}
