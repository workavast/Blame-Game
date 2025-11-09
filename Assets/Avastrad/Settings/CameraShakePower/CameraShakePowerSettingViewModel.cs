using System;
using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    public class CameraShakePowerSettingViewModel : SettingViewModel<CameraShakePowerSettingModel>
    {
        public float ShakePower { get; private set; }
        public override bool HasChanged => !Mathf.Approximately(_model.ShakePower, ShakePower);

        public event Action OnChanged;

        protected override void Initialize()
        {
            ShakePower = _model.ShakePower;
        }

        public override void ApplySettings()
        {
            _model.SetValue(ShakePower);
        }

        public override void ResetSettings()
        {
            ShakePower = _model.ShakePower;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            ShakePower = _model.DefaultShakePower;

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