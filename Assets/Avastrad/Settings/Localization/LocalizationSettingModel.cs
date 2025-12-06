using System;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Avastrad.Settings.Localization
{
    public class LocalizationSettingModel : ISettingModel
    {
        public int SelectedOptionIndex { get; private set; }
        public bool HasChanged { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<Locale> Options { get; private set; }
        public int DefaultOptionIndex { get; private set; }
        public Locale SelectedOption => Options[SelectedOptionIndex];
        
        private readonly LocalizationConfig _config;
        
        public LocalizationSettingModel(LocalizationConfig config)
        {
            _config = config;

            var systemLocale = LocalizationConfig.GetSystemLocale();
            var systemLocaleIndex = LocalizationConfig.GetLocaleIndex(systemLocale);
            
            SelectedOptionIndex = DefaultOptionIndex = systemLocaleIndex;
            Options = LocalizationConfig.GetLocales();
        }

        public void Set(int optionIndex)
        {
            HasChanged = true;
            SelectedOptionIndex = optionIndex;
        }

        public void Apply()
        {
            LocalizationSettings.SelectedLocale = SelectedOption;
            HasChanged = false;
        }

        public void ResetToDefault() 
            => Set(DefaultOptionIndex);

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            Set(state.SelectedOptionIndex);
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