using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.CameraShakePower
{
    public class CameraShakePowerSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private CameraShakePowerSettingsModel Model => _settingsModel.CameraShakePowerSettingsModel;

        public float ShakePower { get; private set; }
        public bool HasChanged => !Mathf.Approximately(Model.ShakePower, ShakePower);

        public Action OnChanged;

        public void Initialize()
        {
            ShakePower = Model.ShakePower;
        }

        public void ApplySettings()
        {
            Model.SetValue(ShakePower);
        }

        public void ResetSettings()
        {
            ShakePower = Model.ShakePower;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            ShakePower = Model.DefaultShakePower;

            ApplySettings();
            
            OnChanged?.Invoke();
        }
        
        public void SetValue(float shakePower, bool notify)
        {
            ShakePower = shakePower;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}