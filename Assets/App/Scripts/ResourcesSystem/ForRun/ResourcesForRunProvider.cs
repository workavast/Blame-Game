using System;
using App.ResourcesSystem.Cells;
using App.ResourcesSystem.ResourcesValues;
using App.ResourcesSystem.Storage;
using UnityEngine;
using Zenject;

namespace App.ResourcesSystem.ForRun
{
    public class ResourcesForRunProvider
    {
        private readonly ResourcesValueConfig _resourcesForWin;
        private readonly ResourcesStorage _resourcesStorage;

        private readonly ResourcesStorage _resourcesStorageForRun = new();
        private readonly ResourcesStorage _resourcesStorageForRunEnd = new();

        public ResourcesForRunProvider(ResourcesValueConfig resourcesForWin, ResourcesStorage resourcesStorage)
        {
            _resourcesForWin = resourcesForWin;
            _resourcesStorage = resourcesStorage;
        }
        
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

        public IReadOnlyResourceStorage GetResourceStorageForEnd() 
            => _resourcesStorageForRunEnd;
        
        public void GameEnded(bool win)
        {
            if (win)
            {
                _resourcesStorage.Add(_resourcesForWin.ResourcesAmount);
                _resourcesStorageForRunEnd.Add(_resourcesForWin.ResourcesAmount);
            }
        }
    }
}