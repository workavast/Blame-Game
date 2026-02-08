using System;
using App.ResourcesSystem.Cells;
using App.ResourcesSystem.Storage;
using UnityEngine;
using Zenject;

namespace App.ResourcesSystem.ForRun
{
    public class ResourcesForRunProvider
    {
        [Inject] private readonly ResourcesStorage _resourcesStorage;

        private readonly ResourcesStorage _resourcesStorageForRun = new();
        
        /// <summary>
        /// return full amount of resource for run
        /// </summary>
        public event Action<ResourceType, int> OnChanged;
        
        public void Add(ResourceType resource, int amount)
        {
            if(amount < 0)
            {
                Debug.LogError("Amount must be greater than 0");
                return;
            }
            
            _resourcesStorage.Add(resource, amount);
            _resourcesStorageForRun.Add(resource, amount);
            
            OnChanged?.Invoke(resource, _resourcesStorageForRun.GetAmount(resource));
        }
        
        public int GetAmount(ResourceType resource) 
            => _resourcesStorageForRun.GetAmount(resource);
        
        public IReadOnlyResourceCell GetResourceCell(ResourceType resource) 
            => _resourcesStorageForRun.GetResourceCell(resource);
        
        public IReadOnlyResourceStorage GetResourceStorage() 
            => _resourcesStorageForRun;
    }
}