using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Avastrad.Settings.AntiAliasing
{
    public class AntiAliasingSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private AntiAliasingSettingsModel Model => _settingsModel.AntiAliasingSettingsModel;
        
        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => Model.AntiAliasingModes;
        public int SelectedAntiAliasingIndex { get; private set; }
        public bool HasChanged => Model.SelectedAntiAliasingIndex != SelectedAntiAliasingIndex;

        public Action OnChanged;

        public void Initialize()
        {
            SelectedAntiAliasingIndex = Model.SelectedAntiAliasingIndex;
        }

        public void ApplySettings()
        {
            Model.SetValue(SelectedAntiAliasingIndex);
        }

        public void ResetSettings()
        {
            SelectedAntiAliasingIndex = Model.SelectedAntiAliasingIndex;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            SelectedAntiAliasingIndex = Model.DefaultAntiAliasingIndex;

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