using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Avastrad.Settings.Localization
{
    [Serializable]
    public class LocalizationSettingsModel : ISettingsModel
    {
        [field: SerializeField] public int SelectedOptionIndex { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<Locale> Options { get; private set; }
        public int DefaultOptionIndex { get; private set; }
        public Locale SelectedOption => Options[SelectedOptionIndex];
        
        private LocalizationConfig _config;
        
        public LocalizationSettingsModel(LocalizationConfig config)
        {
            _config = config;

            var systemLocale = LocalizationConfig.GetSystemLocale();
            var systemLocaleIndex = LocalizationConfig.GetLocaleIndex(systemLocale);
            
            SelectedOptionIndex = DefaultOptionIndex = systemLocaleIndex;
            Options = LocalizationConfig.GetLocales();
        }
        
        public void Load(LocalizationSettingsModel model) 
            => SelectedOptionIndex = model.SelectedOptionIndex;

        public void Set(int optionIndex) 
            => SelectedOptionIndex = optionIndex;

        public void Apply()
        {
            LocalizationSettings.SelectedLocale = SelectedOption;
        }
    }
}