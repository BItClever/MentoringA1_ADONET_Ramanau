using MentoringA1_ADONET_Ramanau.Interfaces;
using MentoringA1_ADONET_Ramanau.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MentoringA1_ADONET_Ramanau
{
    public class UnitOfWork : IDisposable
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailsRepository orderDetailsRepository;
        private readonly IOrderHistoryRepository orderHistoryRepository;
        private ConnectionContext context;
        private bool disposed = false;

        public UnitOfWork(IOrderRepository orderRepository, IOrderHistoryRepository orderHistoryRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailsRepository = orderDetailsRepository;
            this.orderHistoryRepository = orderHistoryRepository;
            SetupContext();
        }

        private void SetupContext()
        {
            context = new ConnectionContext();
            orderRepository.SetupContext(context);
            orderDetailsRepository.SetupContext(context);
            orderHistoryRepository.SetupContext(context);
        }

        public bool AddOrder(Order order)
        {
            return orderRepository.Create(order);
        }

        public bool ChangeOrderDate(DateTime orderDate, int orderID)
        {
            return orderRepository.ChangeOrderDate(orderDate, orderID);
        }

        public bool ChangeShippedDate(DateTime shippedDate, int orderID)
        {
            return orderRepository.ChangeShippedDate(shippedDate, orderID);
        }

        public List<CustOrderHist> CustOrderHist(string customerID)
        {
            return orderHistoryRepository.GetAll(customerID);
        }

        public List<CustOrdersDetails> CustOrdersDetail(string orderID)
        {
            return orderDetailsRepository.GetAll(orderID);
        }

        public Order GetOrderById(int OrderID)
        {
            return orderRepository.Get(OrderID);
        }

        public List<Order> GetAllOrders()
        {
            return orderRepository.GetAll().ToList();
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


