using System.Collections.Generic;

namespace App.Unlocks.Storage
{
    public interface IUnlocksStorage : IReadOnlyUnlocksStorage
    {
        public void Unlock(UnlockConfig unlockConfig);
        public IReadOnlyList<UnlockConfig> GetUnlocks();
        public UnlockState GetState(UnlockConfig unlockConfig);
    }
}