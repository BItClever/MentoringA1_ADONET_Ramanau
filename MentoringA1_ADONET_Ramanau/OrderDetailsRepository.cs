using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class OrderDetailsRepository
    {
        private readonly ConnectionContext context;
        public OrderDetailsRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<CustOrdersDetails> GetAll(string orderId)
        {
            List<CustOrdersDetails> custordersdetails = new List<CustOrdersDetails>();
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandText = "CustOrdersDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@OrderID", SqlDbType.NChar);
            command.Parameters["@OrderID"].Value = orderId;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustOrdersDetails custordersdetail = new CustOrdersDetails();
                custordersdetail.ProductName = (string)reader[0];
                custordersdetail.Price = (decimal)reader[1];
                custordersdetail.Count = (Int16)reader[2];
                custordersdetail.Discount = (int)reader[3];
                custordersdetail.ExtendedPrice = (decimal)reader[4];
                custordersdetails.Add(custordersdetail);
            }
            context.CloseConnection();
            return custordersdetails;
        }
    }
}
