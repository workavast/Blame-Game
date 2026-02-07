using System;
using UnityEngine;
using Zenject;

namespace App.Resources.ForRun
{
    public class ResourcesForRunProvider
    {
        [Inject] private readonly ResourcesStorage _resourcesStorage;

        public int Scrap { get; private set; }
        
        /// <summary>
        /// return full scrap amount for run
        /// </summary>
        public event Action<int> OnScrapChanged;

        public void AddScrap(int amount)
        {
            if(amount < 0)
            {
                Debug.LogError("Amount must be greater than 0");
                return;
            }
            
            Scrap += amount;
            _resourcesStorage.AddScrap(amount);
            
            OnScrapChanged?.Invoke(Scrap);
        }
    }
}