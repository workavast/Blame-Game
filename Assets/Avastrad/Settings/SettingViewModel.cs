using UnityEngine;

namespace Avastrad.Settings
{
    public abstract class SettingViewModel<TSettingModel> : MonoBehaviour, ISettingViewModel
        where TSettingModel : class, ISettingModel
    {
        protected TSettingModel _model;
        
        public abstract bool HasChanged { get; }

        public void Initialize(SettingsRepository settingsRepository)
        {
            _model = settingsRepository.GetSettingModel<TSettingModel>();
            Initialize();
        }

        protected abstract void Initialize();

        public abstract void SetToModel();
        public abstract void ResetSetting();
        public abstract void LoadModelData();
    }
}