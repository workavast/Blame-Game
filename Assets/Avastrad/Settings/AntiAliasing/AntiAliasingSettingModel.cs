using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    public class AntiAliasingSettingModel : ISettingModel
    {
        public int SelectedAntiAliasingIndex { get; private set; }
        public bool HasChanged { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => _config.AntiAliasingModes;
        public int DefaultAntiAliasingIndex => _config.DefaultAntiAliasingIndex;
        public AntialiasingMode SelectedAntialiasingMode => AntiAliasingModes[SelectedAntiAliasingIndex];
        
        private readonly AntiAliasingSettingConfig _config;

        public event Action OnAntialiasingModeChanged;
        
        public AntiAliasingSettingModel(AntiAliasingSettingConfig config)
        {
            _config = config;
            
            SelectedAntiAliasingIndex = DefaultAntiAliasingIndex;
        }

        public void SetValue(int value)
        {
            HasChanged = SelectedAntiAliasingIndex != value;
            SelectedAntiAliasingIndex = value;
        }

        public void Apply()
        {
            HasChanged = false;
            OnAntialiasingModeChanged?.Invoke();
        }

        public void ResetToDefault() 
            => SetValue(DefaultAntiAliasingIndex);

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            SetValue(state.SelectedAntiAliasingIndex);
        }
        
        private struct SettingState : ISettingState
        {
            public int SelectedAntiAliasingIndex { get; set; }
        
            public SettingState(AntiAliasingSettingModel model)
            {
                SelectedAntiAliasingIndex = model.SelectedAntiAliasingIndex;
            }
        }
    }
}