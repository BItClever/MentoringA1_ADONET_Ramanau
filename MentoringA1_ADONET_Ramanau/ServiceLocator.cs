using MentoringA1_ADONET_Ramanau.Interfaces;
using Unity;

namespace MentoringA1_ADONET_Ramanau
{
    public class ServiceLocator
    {
        private readonly IUnityContainer _unityContainer;
        public ServiceLocator()
        {
            _unityContainer = new UnityContainer();
            RegisterDepandencies();
        }

        private void RegisterDepandencies()
        {
            _unityContainer.RegisterType<IOrderDetailsRepository, OrderDetailsRepository>();
            _unityContainer.RegisterType<IOrderRepository, OrderRepository>();
            _unityContainer.RegisterType<IOrderHistoryRepository, OrderHistoryRepository>();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
