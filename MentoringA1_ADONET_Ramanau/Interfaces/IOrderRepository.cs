using MentoringA1_ADONET_Ramanau.Models;
using System;
using System.Collections.Generic;

namespace MentoringA1_ADONET_Ramanau.Interfaces
{
    public interface IOrderRepository : IRepository
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);
        bool Create(Order order);
        bool ChangeOrderDate(DateTime newOrderDate, int orderID);
        bool ChangeShippedDate(DateTime newShippedDate, int orderID);
    }
}
