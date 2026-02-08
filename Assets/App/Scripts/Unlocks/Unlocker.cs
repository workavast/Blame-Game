using App.Resources;
using App.Resources.Storage;
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
            if (_resourcesStorage.HasEnough(unlockConfig.Cost))
            {
                _resourcesStorage.Remove(unlockConfig.Cost);
                _unlocksStorage.Unlock(unlockConfig);
                return true;
            }
            
            //TODO: fix cost ToString
            Debug.Log($"Not enough scrap to unlock:" +
                      $"\n unlock id:[{unlockConfig.Id}]" +
                      $"\n unlock cost:[{unlockConfig.Cost}]" +
                      $"\n current resources:[{_resourcesStorage.ToString()}]");
            
            return false;
        }
    }
}