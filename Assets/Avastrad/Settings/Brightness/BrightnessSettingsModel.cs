using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.Brightness
{
    [Serializable]
    public class BrightnessSettingsModel : ISettingsModel
    {
        [field: SerializeField] public float Value { get; private set; }

        public int Priority => _config.Priority;
        
        public VolumeProfile DefaultVolume => _config.DefaultVolume;
        public float DefaultValue => _config.DefaultValue;
        public float MinValue => _config.MinValue;
        public float MaxValue => _config.MaxValue;

        private readonly BrightnessSettingsConfig _config;
        
        public BrightnessSettingsModel(BrightnessSettingsConfig config)
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

        public void ResetToDefault()
        {
            Value = DefaultValue;
        }

        public void Load(BrightnessSettingsModel model)
        {
            Value = model.Value;
        }

        private void Apply(float value)
        {
            if (DefaultVolume.TryGet(typeof(ColorAdjustments), out ColorAdjustments ca)) 
                ca.postExposure.value = value;
        }
    }
}