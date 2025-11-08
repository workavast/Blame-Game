using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    [Serializable]
    public class ResolutionSettingsModel : ISettingsModel
    {
        [field: SerializeField] public int SelectedResolutionIndex { get; private set; }
        [field: SerializeField] public bool SpecialResolution { get; private set; }
        
        public int Priority => _config.Priority;
        public InspectorResolution MonitorResolution { get; private set; }
        public int DefaultResolutionIndex { get; private set; }
        public IReadOnlyList<InspectorResolution> Resolutions => _resolutions;

        private readonly ResolutionSettingsConfig _config;
        private readonly List<InspectorResolution> _resolutions;
        
        public ResolutionSettingsModel(ResolutionSettingsConfig config)
        {
            _config = config;

            _resolutions = new List<InspectorResolution>(_config.Resolutions);
            
            MonitorResolution = GetMonitorResolution();
            
            DefaultResolutionIndex = _resolutions.IndexOf(MonitorResolution);
            if (DefaultResolutionIndex <= -1)
            {
                SpecialResolution = true;
                _resolutions.Insert(0, MonitorResolution);
                DefaultResolutionIndex = 0;
            }

            SelectedResolutionIndex = DefaultResolutionIndex;
        }

        public void Apply()
        {
            var selectedResolution = Resolutions[SelectedResolutionIndex];
            Screen.SetResolution(selectedResolution.Width, selectedResolution.Height, Screen.fullScreen);
        }
        
        public void Set(int resolutionIndex) 
            => SelectedResolutionIndex = resolutionIndex;

        public void Load(ResolutionSettingsModel model)
        {
            if (SpecialResolution == model.SpecialResolution)
                SelectedResolutionIndex = model.SelectedResolutionIndex;
            else
                SelectedResolutionIndex = DefaultResolutionIndex;
        }

        private static InspectorResolution GetMonitorResolution()
        {
            var firstDisplay = Display.displays[0];
            return new InspectorResolution(firstDisplay.systemWidth, firstDisplay.systemHeight);
        }
    }
}