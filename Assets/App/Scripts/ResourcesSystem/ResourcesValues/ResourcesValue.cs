using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.ResourcesSystem.ResourcesValues
{
    [Serializable]
    public class ResourcesValue
    {
        [SerializeField] private SerializedDictionary<ResourceType, int> resources = new();
        
        public IReadOnlyDictionary<ResourceType, int> Resources => resources;

        public override string ToString() 
            => string.Join(", ", resources);
    }
}