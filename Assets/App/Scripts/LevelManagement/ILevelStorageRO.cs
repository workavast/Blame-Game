using System;

namespace App.LevelManagement
{
    public interface ILevelStorageRO
    {
        public int Level { get; }

        public event Action OnLevelUp;
    }
}