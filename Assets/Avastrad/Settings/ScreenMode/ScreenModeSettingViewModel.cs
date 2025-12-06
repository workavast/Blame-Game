using System;

namespace Avastrad.Settings.ScreenMode
{
    public class ScreenModeSettingViewModel : SettingViewModel<ScreenModeSettingModel>
    {
        public override bool HasChanged => _model.IsFullScreen != IsFullScreen;
        public bool IsFullScreen { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.Set(IsFullScreen);

        public override void ResetSetting() 
            => Set(_model.IsFullScreen, true);

        public override void LoadModelData()
            => Set(_model.IsFullScreen, true);

        public void Set(bool isFullScreen, bool notify)
        {
            IsFullScreen = isFullScreen;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}