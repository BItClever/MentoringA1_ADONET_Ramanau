using MentoringA1_ADONET_Ramanau.Interfaces;
using System;

namespace MentoringA1_ADONET_Ramanau
{
    public class Application
    {
        public void Start()
        {
            var serviceLocator = new ServiceLocator();
            using (var unitOfWork = new UnitOfWork(
                serviceLocator.Resolve<IOrderRepository>(),
                serviceLocator.Resolve<IOrderHistoryRepository>(),
                serviceLocator.Resolve<IOrderDetailsRepository>()))
            {
                var orders = unitOfWork.GetAllOrders();
                var order = unitOfWork.GetOrderById(10248);
                var customerOrderHistory = unitOfWork.CustOrderHist("ALFKI");
                var orderDetails = unitOfWork.CustOrdersDetail("10248");
            }
               
            Console.ReadKey();
        }
    }
}