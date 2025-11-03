using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private VSyncSettingsModel Model => _settingsModel.VSyncSettingsModel;
        
        public bool UseVSync { get; private set; }

        public Action OnChanged;
        
        public void Initialize()
        {
            UseVSync = Model.UseVSync;
        }

        public void ApplySettings()
        {
            Model.SetValue(UseVSync);
        }

        public void ResetSettings()
        {
            UseVSync = Model.UseVSync;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            UseVSync = Model.DefaultValue;

            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(bool value, bool notify)
        {
            UseVSync = value;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}