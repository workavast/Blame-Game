using System;

namespace App.Resources
{
    public interface IReadOnlyResourceStorage
    {
        public int Scrap { get; }
        
        public event Action<int> OnMoneyChanged;

        public bool HasEnoughScrap(int amount);
    }
}