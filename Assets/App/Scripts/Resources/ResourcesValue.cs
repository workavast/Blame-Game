using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Resources
{
    [Serializable]
    public struct ResourcesValue
    {
        [SerializeField] private SerializedDictionary<ResourceType, int> resources;
        
        public IReadOnlyDictionary<ResourceType, int> Resources => resources;

        public override string ToString() 
            => string.Join(", ", resources);
    }
}