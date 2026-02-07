using App.Resources;
using App.Unlocks.Storage;
using UnityEngine;
using Zenject;

namespace App.Unlocks
{
    public class Unlocker
    {
        [Inject] private readonly ResourcesStorage _resourcesStorage;
        [Inject] private readonly UnlockStorage _unlocksStorage;

        public bool TryUnlock(UnlockConfig unlockConfig)
        {
            if (_resourcesStorage.HasEnoughScrap(unlockConfig.Cost))
            {
                _resourcesStorage.RemoveScrap(unlockConfig.Cost);
                _unlocksStorage.Unlock(unlockConfig);
                return true;
            }
            
            Debug.Log($"Not enough scrap to unlock:" +
                      $"\n current scrap:[{_resourcesStorage.Scrap}]" +
                      $"\n unlock id:[{unlockConfig.Id}]" +
                      $"\n unlock cost:[{unlockConfig.Cost}]");
            
            return false;
        }
    }
}