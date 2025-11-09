using System;
using System.Collections.Generic;

namespace Avastrad.Settings.Resolution
{
    public class ResolutionSettingViewModel : SettingViewModel<ResolutionSettingModel>
    {
        public int SelectedResolutionIndex { get; private set; }
        public IReadOnlyList<InspectorResolution> Resolutions => _model.Resolutions;
        public override bool HasChanged => SelectedResolutionIndex != _model.SelectedResolutionIndex;
        
        private int DefaultResolutionIndex => _model.DefaultResolutionIndex;

        public event Action OnChanged;

        protected override void Initialize()
        {
            SelectedResolutionIndex = _model.SelectedResolutionIndex;
        }

        public override void ApplySettings()
        {
            if (!HasChanged)
                return;
            
            _model.Set(SelectedResolutionIndex);
        }
        
        public override void ResetSettings() 
            => Set(_model.SelectedResolutionIndex, true);

        public override void ResetToDefault()
        {
            Set(DefaultResolutionIndex, false);
            ApplySettings();
            
            OnChanged?.Invoke();
        }
        
        public void Set(int resolutionIndex, bool notify)
        {
            if (SelectedResolutionIndex == resolutionIndex)
                return;

            SelectedResolutionIndex = resolutionIndex;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}