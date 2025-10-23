using System;

namespace App.LevelManagement
{
    public class LevelStorage : ILevelStorageRO
    {
        public int Level { get; private set; }

        public event Action OnLevelUp;

        public void LevelUp()
        {
            Level++;
            OnLevelUp?.Invoke();
        }
    }
}