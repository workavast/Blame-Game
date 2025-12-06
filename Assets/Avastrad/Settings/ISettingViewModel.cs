namespace Avastrad.Settings
{
    public interface ISettingViewModel
    {
        public bool HasChanged { get; }

        public void Initialize(SettingsModel settingsModel);
        public void SetToModel();
        public void ResetSetting();
        public void LoadModelData();
    }
}