using System.Data;

namespace App
{
    public static class ServiceLocator
    {
        public static void Add<T>(T service)
            where T : class
        {
            var exist = ServiceLocatorCell<T>.Exist();
            if (exist)
                throw new DuplicateNameException($"Service {typeof(T)} already registered");
            
            ServiceLocatorCell<T>.SetService(service);
        }
        
        public static bool Exist<T>() where T : class 
            => ServiceLocatorCell<T>.Exist();
        
        public static void Remove<T>() where T : class 
            => ServiceLocatorCell<T>.SetService(null);
        
        public static void Remove<T>(T service) where T : class 
            => ServiceLocatorCell<T>.SetService(null);
        
        public static T Get<T>() where T : class 
            => ServiceLocatorCell<T>.Get();

        private static class ServiceLocatorCell<T> 
            where T : class
        {
            private static T _service = null;

            public static void SetService(T service) 
                => _service = service;

            public static bool Exist() 
                => _service != null;

            public static T Get() 
                => _service;
        }
    }
}