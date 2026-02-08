using System;
using UnityEngine;

namespace App.Resources.Cells
{
    public class ResourceCell : IReadOnlyResourceCell
    {
        public ResourceType ResourceType { get; private set; }
        public int Amount { get; private set; }
        public event Action<int> OnChanged;

        public ResourceCell(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }
        
        public bool HasEnough(int amount) 
            => Amount >= amount;

        public void ChangeAmount(int amount)
        {
            Amount += amount;
            
            if (Amount < 0)
            {
                Debug.LogWarning("Cannot have negative amount of resource");
                Amount = 0;
            }
            
            OnChanged?.Invoke(Amount);
        }
    }
}