using App.ResourcesSystem.Storage;
using App.Unlocks.Saves;
using App.Unlocks.Storage;
using UnityEngine;

namespace App.Unlocks
{
    public class Unlocker
    {
        private readonly ResourcesStorage _resourcesStorage;
        private readonly UnlockStorage _unlocksStorage;
        private readonly UnlocksSaveManger _saveManger;

        public Unlocker(ResourcesStorage resourcesStorage, UnlockStorage unlocksStorage,
            UnlocksSaveManger saveManger)
        {
            _resourcesStorage = resourcesStorage;
            _unlocksStorage = unlocksStorage;
            _saveManger = saveManger;
        }

        public bool TryUnlock(UnlockConfig unlockConfig)
        {
            if (_resourcesStorage.HasEnough(unlockConfig.Cost))
            {
                _resourcesStorage.Remove(unlockConfig.Cost);
                _unlocksStorage.Unlock(unlockConfig.Id);
                _saveManger.Save();
                return true;
            }
            
            Debug.Log($"Not enough resources to unlock: [{unlockConfig.Id}]" +
                      $"\n unlock cost:[{unlockConfig.Cost}]" +
                      $"\n current resources:[{_resourcesStorage.ToString()}]");
            
            return false;
        }
    }
}