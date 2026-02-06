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
        [SerializeField] private List<UnlockView> allUnlockViews = new();
        
        [Inject] private readonly IUnlocksStorage _unlocksStorage;
        
        public void Initialize()
        {
            foreach (var view in allUnlockViews)
            {
                var state = _unlocksStorage.GetState(view.GetUnlockConfig());
                view.SetState(state);
                view.OnClick += UnlockPerk;
            }
            
            var popups = GetComponentsInChildren<UnlockPopupActivator>();
            foreach (var popup in popups) 
                popup.Initialize(popupController);
        }

        private void UnlockPerk(UnlockConfig unlockConfig)
        {
            _unlocksStorage.Unlock(unlockConfig);

            UpdateState(unlockConfig);
            foreach (var child in unlockConfig.ChildUnlocks) 
                UpdateState(child);
        }

        private void UpdateState(UnlockConfig unlockConfig)
        {
            foreach (var view in allUnlockViews)
                if (view.GetUnlockConfig() == unlockConfig)
                    view.SetState(_unlocksStorage.GetState(unlockConfig));
        }
    }
}