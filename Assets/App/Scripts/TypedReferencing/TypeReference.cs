using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.TypedReferencing
{
    [Serializable]
    public class TypeReference<T> where T : class
    {
        [SerializeField] private string typeName;
    
        private Type _cachedType;

#if UNITY_EDITOR
        [SerializeField] private float updateTimer;
        [SerializeField] private List<string> cashedDerivedTypes;
#endif
    
        public Type Type
        {
            get
            {
                if (_cachedType == null && !string.IsNullOrEmpty(typeName))
                    _cachedType = Type.GetType(typeName);
                return _cachedType;
            }
            set
            {
                _cachedType = value;
                typeName = value?.AssemblyQualifiedName;
            }
        }
    }
}