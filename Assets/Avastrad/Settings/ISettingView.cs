namespace Avastrad.Settings
{
    public interface ISettingView
    {
        public void Initialize();

        public void OnEnabledManual();
        public void OnDisabledManual();
    }
}