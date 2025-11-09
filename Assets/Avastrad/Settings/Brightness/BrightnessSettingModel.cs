using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.Brightness
{
    public class BrightnessSettingModel : ISettingModel
    {
        public float Value { get; private set; }

        public int Priority => _config.Priority;
        
        public VolumeProfile DefaultVolume => _config.DefaultVolume;
        public float DefaultValue => _config.DefaultValue;
        public float MinValue => _config.MinValue;
        public float MaxValue => _config.MaxValue;

        private readonly BrightnessSettingConfig _config;
        
        public BrightnessSettingModel(BrightnessSettingConfig config)
        {
            _config = config;
            
            Value = DefaultValue;
        }

        public void SetValue(float value)
        {
            Value =  Mathf.Clamp(value, _config.MinValue, _config.MaxValue);
        }
        
        public void SetTemporary(float value)
        {
            value = Mathf.Clamp(value, _config.MinValue, _config.MaxValue);
            Apply(value);
        }
        
        public void Apply() 
            => Apply(Value);

        private void Apply(float value)
        {
            if (DefaultVolume.TryGet(typeof(ColorAdjustments), out ColorAdjustments ca)) 
                ca.postExposure.value = value;
        }
        
        public void ResetToDefault()
        {
            Value = DefaultValue;
        }
        
        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            Value = state.Value;
        }
        
        private struct SettingState : ISettingState
        {
            public float Value { get; set; }
        
            public SettingState(BrightnessSettingModel model)
            {
                Value = model.Value;
            }
        }
    }
}