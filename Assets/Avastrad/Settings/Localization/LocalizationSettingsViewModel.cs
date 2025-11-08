using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Avastrad.Settings.Localization
{
    public class LocalizationSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private readonly SettingsModel _settingsModel;

        public int SelectedOptionIndex { get; private set; }
        public IReadOnlyList<Locale> Options => Model.Options;
        public int DefaultOptionIndex => Model.DefaultOptionIndex;
        public bool HasChanged => Model.SelectedOptionIndex != SelectedOptionIndex;

        private LocalizationSettingsModel Model => _settingsModel.LocalizationSettingsModel;

        public event Action OnChanged;

        public void Initialize()
        {
            SelectedOptionIndex = Model.SelectedOptionIndex;
        }

        public void ApplySettings()
        {
            Model.Set(SelectedOptionIndex);
        }

        public void Set(int optionIndex, bool notify)
        {
            SelectedOptionIndex = optionIndex;
            if (notify)
                OnChanged?.Invoke();
        }

        public void ResetSettings()
        {
            SelectedOptionIndex = Model.SelectedOptionIndex;
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            if (SelectedOptionIndex == DefaultOptionIndex)
                return;

            SelectedOptionIndex = DefaultOptionIndex;
            ApplySettings();

            OnChanged?.Invoke();
        }
    }
}