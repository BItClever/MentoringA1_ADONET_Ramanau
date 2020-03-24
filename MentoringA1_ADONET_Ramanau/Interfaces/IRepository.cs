using System.Collections.Generic;

namespace MentoringA1_ADONET_Ramanau.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        bool Create(T item);
        bool Update(T item);
        void Delete(int id);
    }
}
