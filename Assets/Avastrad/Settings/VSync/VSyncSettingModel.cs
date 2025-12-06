using System;
using UnityEngine;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingModel : ISettingModel
    {
        public bool UseVSync { get; private set; }
        public bool HasChanged { get; private set; }
        
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
            HasChanged = UseVSync != value;
            UseVSync = value;
        }

        public void Apply()
        {
            QualitySettings.vSyncCount = UseVSync ? 1 : 0;
            HasChanged = false;
        }

        public void ResetToDefault() 
            => SetValue(DefaultValue);

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            SetValue(state.UseVSync);
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