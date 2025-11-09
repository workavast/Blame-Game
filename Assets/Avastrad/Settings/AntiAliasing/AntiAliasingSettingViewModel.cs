using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    public class AntiAliasingSettingViewModel : SettingViewModel<AntiAliasingSettingModel>
    {
        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => _model.AntiAliasingModes;
        public int SelectedAntiAliasingIndex { get; private set; }
        public override bool HasChanged => _model.SelectedAntiAliasingIndex != SelectedAntiAliasingIndex;

        public event Action OnChanged;

        protected override void Initialize()
        {
            SelectedAntiAliasingIndex = _model.SelectedAntiAliasingIndex;
        }

        public override void ApplySettings()
        {
            _model.SetValue(SelectedAntiAliasingIndex);
        }

        public override void ResetSettings()
        {
            SelectedAntiAliasingIndex = _model.SelectedAntiAliasingIndex;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            SelectedAntiAliasingIndex = _model.DefaultAntiAliasingIndex;

            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(int antiAliasingIndex, bool notify)
        {
            SelectedAntiAliasingIndex = antiAliasingIndex;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}