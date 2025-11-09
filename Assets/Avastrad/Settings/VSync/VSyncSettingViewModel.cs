using System;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingViewModel : SettingViewModel<VSyncSettingModel>
    {
        public bool UseVSync { get; private set; }
        public override bool HasChanged => _model.UseVSync != UseVSync;

        public event Action OnChanged;

        protected override void Initialize()
        {
            UseVSync = _model.UseVSync;
        }

        public override void ApplySettings()
        {
            _model.SetValue(UseVSync);
        }

        public override void ResetSettings()
        {
            UseVSync = _model.UseVSync;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            UseVSync = _model.DefaultValue;

            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(bool value, bool notify)
        {
            UseVSync = value;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}