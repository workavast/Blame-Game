using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.Brightness
{
    public class BrightnessSettingViewModel : SettingViewModel<BrightnessSettingModel>
    {
        public override bool HasChanged => !Mathf.Approximately(_model.Value, Value);
        public float Value { get; private set; }
        public float MinValue => _model.MinValue;
        public float MaxValue => _model.MaxValue;

        public event Action OnChanged;
        
        protected override void Initialize()
        {
            Value = _model.Value;
        }

        public override void ApplySettings()
        {
            _model.SetValue(Value);
        }

        public override void ResetSettings()
        {
            Value = _model.Value;
            ApplySettings();
            _model.Apply();

            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            Value = _model.DefaultValue;
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(float value, bool notify)
        {
            Value = value;

            _model.SetTemporary(Value);
            
            if (notify)
                OnChanged?.Invoke();
        }
    }
}