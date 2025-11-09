using System;

namespace Avastrad.Settings.ScreenMode
{
    public class ScreenModeSettingViewModel : SettingViewModel<ScreenModeSettingModel>
    {
        public override bool HasChanged => _model.IsFullScreen != IsFullScreen;
        public bool IsFullScreen { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
        {
            IsFullScreen = _model.IsFullScreen;
        }

        public override void ApplySettings()
        {
            if (!HasChanged)
                return;

            _model.Set(IsFullScreen);
        }

        public override void ResetSettings()
        {
            IsFullScreen = _model.IsFullScreen;
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            _model.ResetToDefault();
            IsFullScreen = _model.IsFullScreen;
            
            OnChanged?.Invoke();
        }

        public void Set(bool isFullScreen, bool notify)
        {
            IsFullScreen = isFullScreen;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}