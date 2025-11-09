using UnityEngine;

namespace Avastrad.Settings
{
    public abstract class SettingViewModel<TSettingModel> : MonoBehaviour, ISettingViewModel
        where TSettingModel : class, ISettingModel
    {
        protected TSettingModel _model;
        
        public abstract bool HasChanged { get; }

        public void Initialize(SettingsModel settingsModel)
        {
            _model = settingsModel.GetSettingModel<TSettingModel>();
            Initialize();
        }

        protected abstract void Initialize();

        public abstract void ApplySettings();
        public abstract void ResetSettings();
        public abstract void ResetToDefault();
    }
}