using System.Collections.Generic;

namespace MentoringA1_ADONET_Ramanau.Interfaces
{
    public interface IOrderHistoryRepository : IRepository
    {
        List<CustOrderHist> GetAll(string customerID);
    }
}
