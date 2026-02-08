using System;
using System.Collections.Generic;
using App.Resources.Cells;

namespace App.Resources.Storage
{
    public interface IReadOnlyResourceStorage
    {
        public event Action<ResourceType, int> OnChanged;

        public bool HasEnough(ResourcesValue resources);
        public bool HasEnough(ResourceType resource, int amount);
        public IReadOnlyDictionary<ResourceType, int> GetAmounts(); 
        public int GetAmount(ResourceType resourceType);
        public IReadOnlyResourceCell GetResourceCell(ResourceType resourceType);
    }
}