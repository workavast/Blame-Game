using System;

namespace App.Resources.Cells
{
    public interface IReadOnlyResourceCell
    {
        public ResourceType ResourceType { get; }
        public int Amount { get; }

        /// <summary>
        /// return current amount of resource
        /// </summary>
        public event Action<int> OnChanged;
        
        public bool HasEnough(int amount);
    }
}