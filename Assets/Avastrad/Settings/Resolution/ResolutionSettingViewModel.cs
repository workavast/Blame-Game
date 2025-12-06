using System;
using System.Collections.Generic;

namespace Avastrad.Settings.Resolution
{
    public class ResolutionSettingViewModel : SettingViewModel<ResolutionSettingModel>
    {
        public override bool HasChanged => SelectedResolutionIndex != _model.SelectedResolutionIndex;
        public int SelectedResolutionIndex { get; private set; }
        public IReadOnlyList<InspectorResolution> Resolutions => _model.Resolutions;

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.Set(SelectedResolutionIndex);

        public override void ResetSetting() 
            => Set(_model.SelectedResolutionIndex, true);

        public override void LoadModelData()
            => Set(_model.SelectedResolutionIndex, true);

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