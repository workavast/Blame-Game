using App.ResourcesSystem.Saves;
using App.ResourcesSystem.Storage;
using App.Unlocks.Saves;
using App.Unlocks.Storage;
using UnityEngine;

namespace App.Unlocks
{
    public class Unlocker
    {
        private readonly ResourcesStorage _resourcesStorage;
        private readonly ResourcesSaveManager _resourcesSaveManager;
        private readonly UnlockStorage _unlocksStorage;
        private readonly UnlocksSaveManger _unlocksSaveManger;

        public Unlocker(ResourcesStorage resourcesStorage, ResourcesSaveManager resourcesSaveManager,
            UnlockStorage unlocksStorage, UnlocksSaveManger unlocksSaveManger)
        {
            _resourcesStorage = resourcesStorage;
            _resourcesSaveManager = resourcesSaveManager;
            _unlocksStorage = unlocksStorage;
            _unlocksSaveManger = unlocksSaveManger;
        }

        public bool TryUnlock(UnlockConfig unlockConfig)
        {
            if (_resourcesStorage.HasEnough(unlockConfig.Cost))
            {
                _resourcesStorage.Remove(unlockConfig.Cost);
                _unlocksStorage.Unlock(unlockConfig.Id);
                _resourcesSaveManager.Save();
                _unlocksSaveManger.Save();
                return true;
            }
            
            Debug.Log($"Not enough resources to unlock: [{unlockConfig.Id}]" +
                      $"\n unlock cost:[{unlockConfig.Cost}]" +
                      $"\n current resources:[{_resourcesStorage.ToString()}]");
            
            return false;
        }
    }
}