using System;
using System.Collections.Generic;
using App.ResourcesSystem.Cells;
using App.ResourcesSystem.ResourcesValues;

namespace App.ResourcesSystem.Storage
{
    public interface IReadOnlyResourceStorage
    {
        public event Action OnChanged;

        public bool HasEnough(ResourcesValue resources);
        public bool HasEnough(ResourceType resource, int amount);
        public IReadOnlyDictionary<ResourceType, int> GetAmounts(); 
        public int GetAmount(ResourceType resourceType);
        public IReadOnlyResourceCell GetResourceCell(ResourceType resourceType);
    }
}