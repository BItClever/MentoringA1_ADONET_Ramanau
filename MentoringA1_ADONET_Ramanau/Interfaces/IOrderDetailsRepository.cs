using System.Collections.Generic;

namespace MentoringA1_ADONET_Ramanau.Interfaces
{
    public interface IOrderDetailsRepository : IRepository
    {
        List<CustOrdersDetails> GetAll(string orderId);
    }
}
