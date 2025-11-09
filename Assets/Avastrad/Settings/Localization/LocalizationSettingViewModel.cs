using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Avastrad.Settings.Localization
{
    public class LocalizationSettingViewModel : SettingViewModel<LocalizationSettingModel>
    {
        public int SelectedOptionIndex { get; private set; }
        public IReadOnlyList<Locale> Options => _model.Options;
        public int DefaultOptionIndex => _model.DefaultOptionIndex;
        public override bool HasChanged => _model.SelectedOptionIndex != SelectedOptionIndex;

        public event Action OnChanged;

        protected override void Initialize()
        {
            SelectedOptionIndex = _model.SelectedOptionIndex;
        }

        public override void ApplySettings()
        {
            _model.Set(SelectedOptionIndex);
        }
        
        public override void ResetSettings()
        {
            SelectedOptionIndex = _model.SelectedOptionIndex;
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            if (SelectedOptionIndex == DefaultOptionIndex)
                return;

            SelectedOptionIndex = DefaultOptionIndex;
            ApplySettings();

            OnChanged?.Invoke();
        }
        
        public void Set(int optionIndex, bool notify)
        {
            SelectedOptionIndex = optionIndex;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}