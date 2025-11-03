using System;
using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    [Serializable]
    public class CameraShakePowerSettingsModel : ISettingsModel
    {
        [field: SerializeField] public float ShakePower { get; private set; }

        public int Priority => _config.Priority;
        public float DefaultShakePower => _config.DefaultShakePower;
        
        private readonly CameraShakePowerSettingsConfig _config;
        
        public CameraShakePowerSettingsModel(CameraShakePowerSettingsConfig config)
        {
            _config = config;
            
            ShakePower = DefaultShakePower;
        }

        public void SetValue(float shakePower) 
            => ShakePower = shakePower;

        public void Apply() { }

        public void ResetToDefault() 
            => ShakePower = DefaultShakePower;

        public void Load(CameraShakePowerSettingsModel model) 
            => ShakePower = model.ShakePower;
    }
}