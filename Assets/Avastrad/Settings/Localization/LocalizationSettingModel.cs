using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Avastrad.Settings.Localization
{
    [Serializable]
    public class LocalizationSettingModel : ISettingModel
    {
        [field: SerializeField] public int SelectedOptionIndex { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<Locale> Options { get; private set; }
        public int DefaultOptionIndex { get; private set; }
        public Locale SelectedOption => Options[SelectedOptionIndex];
        
        private LocalizationConfig _config;
        
        public LocalizationSettingModel(LocalizationConfig config)
        {
            _config = config;

            var systemLocale = LocalizationConfig.GetSystemLocale();
            var systemLocaleIndex = LocalizationConfig.GetLocaleIndex(systemLocale);
            
            SelectedOptionIndex = DefaultOptionIndex = systemLocaleIndex;
            Options = LocalizationConfig.GetLocales();
        }
        
        public void Load(LocalizationSettingModel model) 
            => SelectedOptionIndex = model.SelectedOptionIndex;

        public void Set(int optionIndex) 
            => SelectedOptionIndex = optionIndex;

        public void Apply()
        {
            LocalizationSettings.SelectedLocale = SelectedOption;
        }

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            SelectedOptionIndex = state.SelectedOptionIndex;
        }
        
        private struct SettingState : ISettingState
        {
            public int SelectedOptionIndex { get; set; }
        
            public SettingState(LocalizationSettingModel model)
            {
                SelectedOptionIndex = model.SelectedOptionIndex;
            }
        }
    }
}