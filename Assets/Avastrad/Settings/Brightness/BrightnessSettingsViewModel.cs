using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.Brightness
{
    public class BrightnessSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private BrightnessSettingsModel Model => _settingsModel.BrightnessSettingsModel;
        
        public float Value { get; private set; }
        public float MinValue => Model.MinValue;
        public float MaxValue => Model.MaxValue;

        public Action OnChanged;
        
        public void Initialize()
        {
            Value = Model.Value;
        }

        public void ApplySettings()
        {
            Model.SetValue(Value);
        }

        public void ResetSettings()
        {
            Value = Model.Value;
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            Value = Model.DefaultValue;
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(float value, bool notify)
        {
            Value = value;

            Model.SetTemporary(Value);
            
            if (notify)
                OnChanged?.Invoke();
        }
    }
}