using System;

namespace Avastrad.Settings.VSync
{
    public class VSyncSettingViewModel : SettingViewModel<VSyncSettingModel>
    {
        public override bool HasChanged => _model.UseVSync != UseVSync;
        public bool UseVSync { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.SetValue(UseVSync);

        public override void ResetSetting() 
            => SetValue(_model.UseVSync, true);

        public override void LoadModelData()
            => SetValue(_model.UseVSync, true);

        public void SetValue(bool value, bool notify)
        {
            if (UseVSync == value)
                return;
            
            UseVSync = value;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}