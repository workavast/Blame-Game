namespace Avastrad.Settings
{
    public interface ISettingViewModel
    {
        public bool HasChanged { get; }

        public void Initialize(SettingsModel settingsModel);
        public void ApplySettings();
        public void ResetSettings();
        public void ResetToDefault();
    }
}