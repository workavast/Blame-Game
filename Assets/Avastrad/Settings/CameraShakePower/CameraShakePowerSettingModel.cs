using System;
using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    public class CameraShakePowerSettingModel : ISettingModel
    {
        public float ShakePower { get; private set; }

        public int Priority => _config.Priority;
        public float DefaultShakePower => _config.DefaultShakePower;
        
        private readonly CameraShakePowerSettingConfig _config;
        
        public CameraShakePowerSettingModel(CameraShakePowerSettingConfig config)
        {
            _config = config;
            
            ShakePower = DefaultShakePower;
        }

        public void SetValue(float shakePower) 
            => ShakePower = shakePower;

        public void Apply() { }

        public void ResetToDefault() 
            => ShakePower = DefaultShakePower;

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            ShakePower = state.ShakePower;
        }
        
        private struct SettingState : ISettingState
        {
            public float ShakePower { get; set; }
        
            public SettingState(CameraShakePowerSettingModel model)
            {
                ShakePower = model.ShakePower;
            }
        }
    }
}