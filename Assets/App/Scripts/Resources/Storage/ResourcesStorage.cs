using System;
using System.Collections.Generic;
using System.Linq;
using App.Resources.Cells;
using Avastrad.Libs.EnumValuesLib;
using UnityEngine;

namespace App.Resources.Storage
{
    public class ResourcesStorage : IReadOnlyResourceStorage
    {
        private readonly Dictionary<ResourceType, ResourceCell> _resources;
        
        public event Action<ResourceType, int> OnChanged;

        public ResourcesStorage()
        {
            var resourceTypes = EnumValuesTool.GetValues<ResourceType>();
            _resources = new Dictionary<ResourceType, ResourceCell>(resourceTypes.Count());
            foreach (var resourceType in resourceTypes) 
                _resources.Add(resourceType, new ResourceCell(resourceType));
        }
        
        public bool HasEnough(ResourcesValue resources)
        {
            foreach (var resource in resources.Resources)
                if (!HasEnough(resource.Key, resource.Value))
                    return false;

            return true;
        }
        
        public bool HasEnough(ResourceType resource, int amount) 
            => _resources[resource].HasEnough(amount);
        
        public void Add(ResourcesValue resources)
        {
            foreach (var resource in resources.Resources)
                Add(resource.Key, resource.Value);

            foreach (var resource in resources.Resources)
                OnChanged?.Invoke(resource.Key, GetAmount(resource.Key));//TODO: decrease event calls
        }
        
        public void Add(ResourceType resourceType, int amount)
        {
            if (amount < 0)
            {
                Debug.LogError($"Cannot add negative amount of resource: [{resourceType}] [{amount}]");
                return;
            }
            
            _resources[resourceType].ChangeAmount(amount);
            OnChanged?.Invoke(resourceType, _resources[resourceType].Amount);
        }
        
        public void Remove(ResourcesValue resources)
        {
            foreach (var resource in resources.Resources)
                Remove(resource.Key, resource.Value);
            
            foreach (var resource in resources.Resources)
                OnChanged?.Invoke(resource.Key, GetAmount(resource.Key));//TODO: decrease event calls
        }
        
        public void Remove(ResourceType resourceType, int amount)
        {
            if (amount < 0)
            {
                Debug.LogError($"Cannot remove negative amount of resource: [{resourceType}] [{amount}]");
                return;
            }
            
            _resources[resourceType].ChangeAmount(-amount);
            OnChanged?.Invoke(resourceType, _resources[resourceType].Amount);
        }
        
        public IReadOnlyDictionary<ResourceType, int> GetAmounts() 
            => _resources.ToDictionary(x => x.Key, x => x.Value.Amount);

        public int GetAmount(ResourceType resourceType)
            => _resources[resourceType].Amount;

        public IReadOnlyResourceCell GetResourceCell(ResourceType resourceType) 
            => _resources[resourceType];
        
        public override string ToString() 
            => string.Join(", ", _resources.Select(x => $"{x.Key}: {x.Value.Amount}"));
    }
}