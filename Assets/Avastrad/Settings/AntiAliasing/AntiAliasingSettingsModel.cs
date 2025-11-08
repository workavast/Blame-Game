using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    [Serializable]
    public class AntiAliasingSettingsModel : ISettingsModel
    {
        [field: SerializeField] public int SelectedAntiAliasingIndex { get; private set; }

        public int Priority => _config.Priority;
        
        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => _config.AntiAliasingModes;
        public int DefaultAntiAliasingIndex => _config.DefaultAntiAliasingIndex;
        public AntialiasingMode SelectedAntialiasingMode => AntiAliasingModes[SelectedAntiAliasingIndex];
        
        private readonly AntiAliasingSettingsConfig _config;

        public event Action OnAntialiasingModeChanged;
        
        public AntiAliasingSettingsModel(AntiAliasingSettingsConfig config)
        {
            _config = config;
            
            SelectedAntiAliasingIndex = DefaultAntiAliasingIndex;
        }

        public void SetValue(int value)
        {
            SelectedAntiAliasingIndex = value;
        }
        
        public void Apply()
        {
            OnAntialiasingModeChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            SelectedAntiAliasingIndex = DefaultAntiAliasingIndex;
        }

        public void Load(AntiAliasingSettingsModel model)
        {
            SelectedAntiAliasingIndex = model.SelectedAntiAliasingIndex;
        }
    }
}