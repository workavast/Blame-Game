namespace Avastrad.Settings
{
    public interface ISettingsViewModel
    {
        public bool HasChanged { get; }

        public void Initialize();
        public void ApplySettings();
        public void ResetSettings();
        public void ResetToDefault();
    }
}