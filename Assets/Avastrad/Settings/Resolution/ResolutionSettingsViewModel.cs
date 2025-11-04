using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.Resolution
{
    public class ResolutionSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;
        
        public int SelectedResolutionIndex { get; private set; }
        public IReadOnlyList<InspectorResolution> Resolutions => Model.Resolutions;
        public bool HasChanged => SelectedResolutionIndex != Model.SelectedResolutionIndex;
        
        private ResolutionSettingsModel Model => _settingsModel.ResolutionSettingsModel;
        private int DefaultResolutionIndex => Model.DefaultResolutionIndex;

        public event Action OnChanged;

        public void Initialize()
        {
            SelectedResolutionIndex = Model.SelectedResolutionIndex;
        }

        public void ApplySettings()
        {
            if (!HasChanged)
                return;
            
            Model.Set(SelectedResolutionIndex);
        }

        public void Set(int resolutionIndex, bool notify)
        {
            if (SelectedResolutionIndex == resolutionIndex)
                return;

            SelectedResolutionIndex = resolutionIndex;
            if (notify)
                OnChanged?.Invoke();
        }
        
        public void ResetSettings() 
            => Set(Model.SelectedResolutionIndex, true);

        public void ResetToDefault()
        {
            Set(DefaultResolutionIndex, false);
            ApplySettings();
            
            OnChanged?.Invoke();
        }
    }
}