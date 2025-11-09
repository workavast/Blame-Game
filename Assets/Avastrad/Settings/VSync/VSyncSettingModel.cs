using System;
using UnityEngine;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingModel : ISettingModel
    {
        public bool UseVSync { get; private set; }

        public int Priority => _config.Priority;
        public bool DefaultValue => _config.DefaultValue;
        
        private readonly VSyncSettingConfig _config;
        
        public VSyncSettingModel(VSyncSettingConfig config)
        {
            _config = config;
            
            UseVSync = DefaultValue;
        }

        public void SetValue(bool value)
        {
            UseVSync = value;
        }
        
        public void Apply()
        {
            QualitySettings.vSyncCount = UseVSync ? 1 : 0;
        }

        public void ResetToDefault()
        {
            UseVSync = DefaultValue;
        }
        
        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            UseVSync = state.UseVSync;
        }
        
        private struct SettingState : ISettingState
        {
            public bool UseVSync { get; set; }
        
            public SettingState(VSyncSettingModel model)
            {
                UseVSync = model.UseVSync;
            }
        }
    }
}