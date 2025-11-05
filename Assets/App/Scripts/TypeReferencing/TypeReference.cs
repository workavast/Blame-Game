using System;
using UnityEngine;

namespace App.TypeReferencing
{
    [Serializable]
    public class TypeReference<T> where T : class
    {
        [SerializeField] private string typeName;
    
        private Type _cachedType;

        public Type Type
        {
            get
            {
                if (_cachedType == null && !string.IsNullOrEmpty(typeName))
                    _cachedType = Type.GetType(typeName);
                return _cachedType;
            }
        }
    }
}