using System;
using UnityEngine;

namespace App.Resources
{
    public class ResourcesStorage : IReadOnlyResourceStorage
    {
        public int Scrap { get; private set; }
        public event Action<int> OnMoneyChanged;

        public bool HasEnoughScrap(int amount) 
            => Scrap >= amount;

        public void AddScrap(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot add negative amount of scrap");
                return;
            }
            
            Scrap += amount;
            OnMoneyChanged?.Invoke(Scrap);
        }
        
        public void RemoveScrap(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot remove negative amount of scrap");
                return;
            }
            
            Scrap -= amount;
            
            if (Scrap < 0)
            {
                Debug.LogError("Cannot remove more scrap than available");
                Scrap = 0;
            }
            OnMoneyChanged?.Invoke(Scrap);
        }
    }
}