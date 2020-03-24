using MentoringA1_ADONET_Ramanau.Interfaces;
using MentoringA1_ADONET_Ramanau.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MentoringA1_ADONET_Ramanau
{
    public class UnitOfWork : IDisposable
    {
        public IRepository<Product> ProductRepository { get; set; }
        public OrderRepository OrderRepository { get; set; }
        public OrderDetailsRepository OrderDetailsRepository { get; set; }
        public OrderHistoryRepository OrderHistoryRepository { get; set; }
        private readonly ConnectionContext context;
        private bool disposed = false;

        public UnitOfWork()
        {
            context = new ConnectionContext();
            OrderRepository = new OrderRepository(context);
            OrderDetailsRepository = new OrderDetailsRepository(context);
            OrderHistoryRepository = new OrderHistoryRepository(context);
        }

        public bool AddOrder(Order order)
        {
            return OrderRepository.Create(order);
        }

        public bool ChangeOrderDate(DateTime orderDate, int orderID)
        {
            return OrderRepository.ChangeOrderDate(orderDate, orderID);
        }

        public bool ChangeShippedDate(DateTime shippedDate, int orderID)
        {
            return OrderRepository.ChangeShippedDate(shippedDate, orderID);
        }

        public List<CustOrderHist> CustOrderHist(string customerID)
        {
            return OrderHistoryRepository.GetAll(customerID);
        }

        public List<CustOrdersDetails> CustOrdersDetail(string orderID)
        {
            return OrderDetailsRepository.GetAll(orderID);
        }

        public Order GetOrderById(int OrderID)
        {
            return OrderRepository.Get(OrderID);
        }

        public List<Order> GetAllOrders()
        {
            return OrderRepository.GetAll().ToList();
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
                    context.Dispose();
                }
                disposed = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}


