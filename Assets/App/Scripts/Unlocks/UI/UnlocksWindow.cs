using System.Collections.Generic;
using App.UI.Popup;
using App.Unlocks.Storage;
using UnityEngine;
using Zenject;

namespace App.Unlocks.UI
{
    public class UnlocksWindow : MonoBehaviour
    {
        [SerializeField] private PopupController popupController;
        
        [Inject] private readonly UnlockStorage _unlocksStorage;
        [Inject] private readonly Unlocker _unlocker;
        
        private IReadOnlyList<UnlockView> _views;
        
        public void Initialize(IReadOnlyList<UnlockView> views)
        {
            _views = views;
            
            foreach (var view in _views)
            {
                var state = _unlocksStorage.GetState(view.GetUnlockConfig());
                view.SetState(state);
                view.OnClick += TryUnlockPerk;
            }
            
            var popups = GetComponentsInChildren<UnlockPopupActivator>();
            foreach (var popup in popups) 
                popup.Initialize(popupController);
        }

        private void TryUnlockPerk(UnlockConfig unlockConfig)
        {
            if (_unlocksStorage.GetState(unlockConfig) == UnlockState.Unlocked)
            {
                Debug.LogWarning("Perk is already unlocked");
                return;
            }

            if (!_unlocker.TryUnlock(unlockConfig))
            {
                return;
            }
            
            UpdateState(unlockConfig);
            foreach (var child in unlockConfig.ChildUnlocks) 
                UpdateState(child);
        }

        private void UpdateState(UnlockConfig unlockConfig)
        {
            foreach (var view in _views)
                if (view.GetUnlockConfig() == unlockConfig)
                    view.SetState(_unlocksStorage.GetState(unlockConfig));
        }
    }
}