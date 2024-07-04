using System.Collections.Generic;

namespace App.Services
{
    public class AppServiceContainer
    {
        private readonly List<IAppService> _services = new List<IAppService>();

        public void RegisterSingle<TService>(TService implementation) where TService : class, IAppService
        {
            Implementation<TService>.ServiceInstance = implementation;
            _services.Add(implementation);
        }

        public TService Single<TService>() where TService : class, IAppService =>
            Implementation<TService>.ServiceInstance;

        public void Cleanup()
        {
            foreach (IAppService service in _services)
            {
                service.Cleanup();
            }

            _services.Clear();
        }

        private static class Implementation<TService> where TService : class, IAppService
        {
            public static TService ServiceInstance;
        }
    }
}
