using MentoringA1_ADONET_Ramanau.Interfaces;
using MentoringA1_ADONET_Ramanau.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class UnitOfWork : IDisposable
    {

        public IRepository<Product> ProductRepository { get; set; }
        public IRepository<Order> OrderRepository { get; set; }
        private readonly ConnectionContext context;
        private bool disposed = false;

        public UnitOfWork()
        {
            ProductRepository = new ProductRepository();
            OrderRepository = new OrderRepository();
            context = new ConnectionContext();
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
                    context.Dispose();
                }
                disposed = true;
            }
        }


        public bool AddOrder(Order order)
        {
            var command = context.Connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = String.Format("INSERT INTO Orders (OrderDate,RequiredDate,ShippedDate,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) " +
                                  "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                  order.OrderDate, order.RequiredDate, order.ShippedDate, order.ShipName, order.ShipAddress, order.ShipCity, order.ShipRegion, order.ShipPostalCode, order.ShipCountry);
            var num = command.ExecuteNonQuery();
            return (num > 0);

        }

        public bool ChangeOrderDate(DateTime OrderDate, int OrderID)
        {
            var command = context.Connection.CreateCommand();
            command.CommandText = String.Format("UPDATE Orders SET OrderDate={0} WHERE OrderID='{1}'", OrderDate.ToString("yyyy-mm-dd"), OrderID);
            command.CommandType = CommandType.Text;

            return (command.ExecuteNonQuery() > 0);
        }

        public bool ChangeShippedDate(DateTime ShippedDate, int OrderID)
        {
            var command = context.Connection.CreateCommand();
            command.CommandText = String.Format("UPDATE Orders SET ShippedDate={0} WHERE OrderID={1}", ShippedDate.ToString("yyyy-mm-dd"), OrderID);
            command.CommandType = CommandType.Text;

            return (command.ExecuteNonQuery() > 0);

        }

        public List<CustOrderHist> CustOrderHist(string CustomerID)
        {
            Dictionary<Product, int> custorderhist = new Dictionary<Product, int>();
            List<CustOrderHist> orderHist = new List<CustOrderHist>();

            var command = context.Connection.CreateCommand();
            command.CommandText = "CustOrderHist";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CustomerID", SqlDbType.NChar);
            command.Parameters["@CustomerID"].Value = CustomerID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustOrderHist or = new CustOrderHist();
                or.ProductName = reader.GetString(0);
                or.Total = reader.GetInt32(1);
                orderHist.Add(or);
            }

            return orderHist;
        }

        public List<CustOrdersDetails> CustOrdersDetail(string OrderID)
        {
            List<CustOrdersDetails> custordersdetails = new List<CustOrdersDetails>();
            var command = context.Connection.CreateCommand();
            command.CommandText = "CustOrdersDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@OrderID", SqlDbType.NChar);
            command.Parameters["@OrderID"].Value = OrderID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustOrdersDetails custordersdetail = new CustOrdersDetails();
                custordersdetail.ProductName = (string)reader[0];
                custordersdetail.Price = (decimal)reader[1];
                custordersdetail.Count = (int)reader[2];
                custordersdetail.Discount = (int)reader[3];
                custordersdetail.ExtendedPrice = (decimal)reader[4];
                custordersdetails.Add(custordersdetail);
            }

            return custordersdetails;
        }

        public Order GetOrderById(int OrderID)
        {
            Order order = new Order();

            var command = context.Connection.CreateCommand();
            command.CommandText = String.Format("SELECT Orders.*, Products.ProductID, Products.ProductName FROM Orders LEFT JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID LEFT JOIN Products ON[Order Details].ProductID = Products.ProductID WHERE Orders.OrderID={0}", OrderID);
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

            return order;

        }

        public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();

            var command = context.Connection.CreateCommand();
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

            return list;
        }
    }

}


