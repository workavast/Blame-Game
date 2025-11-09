using System;
using UnityEngine;

namespace Avastrad.Settings.ScreenMode
{
    public class ScreenModeSettingModel : ISettingModel
    {
        public bool IsFullScreen { get; private set; }

        public int Priority => _config.Priority;
        public bool DefaultIsFullScreen => _config.DefaultIsFullScreen;

        private readonly ScreenModeConfig _config;

        public ScreenModeSettingModel(ScreenModeConfig config)
        {
            _config = config;
            IsFullScreen = DefaultIsFullScreen;
        }

        public void Set(bool isFullScreen) 
            => IsFullScreen = isFullScreen;

        public void Apply() 
            => Screen.fullScreen = IsFullScreen;

        public void ResetToDefault()
            => Set(DefaultIsFullScreen);
        
        public Type GetStateType() 
            => typeof(ScreenModeSettingState);

        public ISettingState GetState() 
            => new ScreenModeSettingState(this);
        
        public void LoadState(ISettingState genericState)
        {
            var state = (ScreenModeSettingState)genericState;
            IsFullScreen = state.IsFullScreen;
        }
        
        private struct ScreenModeSettingState : ISettingState
        {
            public bool IsFullScreen { get; set; }

            public ScreenModeSettingState(ScreenModeSettingModel model)
            {
                IsFullScreen = model.IsFullScreen;
            }
        }
    }
}