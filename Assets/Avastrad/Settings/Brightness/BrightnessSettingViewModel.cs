using System;
using UnityEngine;

namespace Avastrad.Settings.Brightness
{
    public class BrightnessSettingViewModel : SettingViewModel<BrightnessSettingModel>
    {
        public override bool HasChanged => !Mathf.Approximately(_model.Value, Value);
        public float MinValue => _model.MinValue;
        public float MaxValue => _model.MaxValue;
        public float Value { get; private set; }

        public event Action OnChanged;
        
        protected override void Initialize() 
            => LoadModelData();

        public override void SetToModel() 
            => _model.SetValue(Value);

        public override void ResetSetting() 
            => SetValue(_model.Value, true);

        public override void LoadModelData() 
            => SetValue(_model.Value, true);

        public void SetValue(float value, bool notify)
        {
            Value = value;
            _model.SetTemporary(Value);
            
            if (notify)
                OnChanged?.Invoke();
        }
    }
}