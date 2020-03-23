using System;

namespace MentoringA1_ADONET_Ramanau
{
    public class Application
    {
        public void Start()
        {
            using (var unitOfWork = new UnitOfWork())
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