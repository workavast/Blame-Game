using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.ScreenMode
{
    public class ScreenModeSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private readonly SettingsModel _settingsModel;

        public bool IsChanged => Model.IsFullScreen != IsFullScreen;
        public bool IsFullScreen { get; private set; }

        private ScreenModeSettingsModel Model => _settingsModel.ScreenModeSettingsModel;

        public event Action OnChanged;

        public void Initialize()
        {
            IsFullScreen = Model.IsFullScreen;
        }

        public void ApplySettings()
        {
            if (!IsChanged)
                return;

            Model.Set(IsFullScreen);
        }

        public void ResetSettings()
        {
            IsFullScreen = Model.IsFullScreen;
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            Model.ResetToDefault();
            IsFullScreen = Model.IsFullScreen;
            
            OnChanged?.Invoke();
        }

        public void Set(bool isFullScreen, bool notify)
        {
            IsFullScreen = isFullScreen;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}