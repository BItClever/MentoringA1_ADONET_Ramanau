using MentoringA1_ADONET_Ramanau.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class OrderRepository
    {
        private readonly ConnectionContext context;

        public OrderRepository(ConnectionContext context)
        {
            this.context = context;
        }
        public bool Create(Order order)
        {
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = String.Format("INSERT INTO Orders (OrderDate,RequiredDate,ShippedDate,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) " +
                                  "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                  order.OrderDate, order.RequiredDate, order.ShippedDate, order.ShipName, order.ShipAddress, order.ShipCity, order.ShipRegion, order.ShipPostalCode, order.ShipCountry);
            var num = command.ExecuteNonQuery();
            context.CloseConnection();
            return (num > 0);
        }


        public Order Get(int id)
        {
            Order order = new Order();
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandText = String.Format("SELECT Orders.*, Products.ProductID, Products.ProductName FROM Orders LEFT JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID LEFT JOIN Products ON[Order Details].ProductID = Products.ProductID WHERE Orders.OrderID={0}", id);
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                order.OrderID = (int)reader[0];
                order.CustomerID = (string)reader[1];
                order.EmployeeID = reader[2].ToString();
                order.OrderDate = (DateTime)reader[3];
                order.RequiredDate = (DateTime)reader[4];
                order.ShippedDate = (DateTime)reader[5];
                order.ShipVia = reader[6].ToString();
                order.Freight = (decimal)reader[7];
                order.ShipName = (string)reader[8];
                order.ShipAddress = (string)reader[9];
                order.ShipCity = (string)reader[10];
                order.ShipRegion = reader[11].ToString();
                order.ShipPostalCode = (string)reader[12];
                order.ShipCountry = (string)reader[13];
                order.OrderedProduct.ProductID = (int)reader[14];
                order.OrderedProduct.ProductName = (string)reader[15];
            }
            context.CloseConnection();
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> list = new List<Order>();
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandText = "SELECT * FROM Orders";
            command.CommandType = CommandType.Text;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Order order = new Order();

                order.OrderID = (int)reader[0];
                order.CustomerID = reader[1].ToString();
                order.EmployeeID = reader[2].ToString();
                order.OrderDate = (DateTime)reader[3];
                order.RequiredDate = (DateTime)reader[4];

                if (reader[5].ToString().Equals(null))
                {
                    order.ShippedDate = Convert.ToDateTime(reader[5]);
                }

                order.ShipVia = reader[6].ToString();
                order.Freight = (decimal)reader[7];
                order.ShipName = (string)reader[8];
                order.ShipAddress = (string)reader[9];
                order.ShipCity = (string)reader[10];
                order.ShipRegion = reader[11].ToString();
                order.ShipPostalCode = reader[12].ToString();
                order.ShipCountry = (string)reader[13];
                list.Add(order);
            }
            context.CloseConnection();
            return list;
        }

        public bool ChangeOrderDate(DateTime newOrderDate, int orderID)
        {
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandText = String.Format("UPDATE Orders SET OrderDate={0} WHERE OrderID='{1}'", newOrderDate.ToString("yyyy-mm-dd"), orderID);
            command.CommandType = CommandType.Text;
            context.CloseConnection();
            return (command.ExecuteNonQuery() > 0);
        }

        public bool ChangeShippedDate(DateTime newShippedDate, int orderID)
        {
            context.OpenConnection();
            var command = context.CreateCommand();
            command.CommandText = String.Format("UPDATE Orders SET ShippedDate={0} WHERE OrderID={1}", newShippedDate.ToString("yyyy-mm-dd"), orderID);
            command.CommandType = CommandType.Text;
            context.CloseConnection();
            return (command.ExecuteNonQuery() > 0);

        }
    }
}
