using System;
using Avastrad.Settings.Save;

namespace Avastrad.Settings
{
    public class SettingsModel
    {
        private readonly ISettingModel[] _allSettings;

        public SettingsModel(SettingsConfigsRepository configsRepository)
        {
            _allSettings = new ISettingModel[configsRepository.ConfigsRO.Count];
            for (int i = 0; i < configsRepository.ConfigsRO.Count; i++) 
                _allSettings[i] = configsRepository.ConfigsRO[i].CreateModel();
            
            Array.Sort(_allSettings, (x, y) => -x.Priority.CompareTo(y.Priority));
        }

        public TSettingModel GetSettingModel<TSettingModel>()
            where TSettingModel : class, ISettingModel
        {
            foreach (var settingModel in _allSettings)
                if (settingModel.GetType() == typeof(TSettingModel))
                    return (TSettingModel)settingModel;

            return null;
        }

        public void TryLoad()
        {
            if (SettingsSaver.HasValidSave<ISettingState[]>())
                Load();
        }
        
        public void Load()
        {
            var modelsStates = SettingsSaver.Load<ISettingState[]>();
            foreach (var modelState in modelsStates)
            {
                var type = modelState.GetType();
                foreach (var settingModel in _allSettings)
                    if (settingModel.GetStateType() == type) 
                        settingModel.LoadState(modelState);
            }
        }

        public void Save()
        {
            var states = new ISettingState[_allSettings.Length];
            for (var i = 0; i < _allSettings.Length; i++) 
                states[i] = _allSettings[i].GetState();
            
            SettingsSaver.Save(states);
        }

        public void Apply(bool force)
        {
            foreach (var settings in _allSettings)
                if (force || settings.HasChanged) 
                    settings.Apply();
        }
        
        public void ResetToDefault()
        {
            foreach (var setting in _allSettings) 
                setting.ResetToDefault();
        }
    }
}