using System;

namespace MentoringA1_ADONET_Ramanau
{
    public class UnitOfWork : IDisposable
    {
        public ProductRepository ProductRepository { get; set; }
        public OrderRepository OrderRepository { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
