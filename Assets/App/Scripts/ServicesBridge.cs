using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace App
{
    public class ServicesBridge
    {
        private readonly Dictionary<Type, object> _serviceCells = new();

        private static ServicesBridge _instance;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize() 
            => _instance = new ServicesBridge();

        public static void Add<T>(T service)
        {
            if (Exist<T>())
                throw new DuplicateNameException($"Service {typeof(T)} already registered");
            
            _instance._serviceCells.Add(typeof(T), service);
        }
        
        public static bool Exist<T>() 
            => _instance._serviceCells.ContainsKey(typeof(T));

        public static void Remove<T>(T service)
            => _instance._serviceCells.Remove(typeof(T));
        
        public static T Get<T>()
            => (T)_instance._serviceCells[typeof(T)];
    }
}