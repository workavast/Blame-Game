using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.Fps
{
    public class FpsSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private readonly SettingsModel _settingsModel;

        public int SelectedOptionIndex { get; private set; }
        public IReadOnlyList<int> FpsOptions => Model.FpsOptions;
        public int DefaultOptionIndex => Model.DefaultOptionIndex;
        public bool HasChanged => Model.SelectedOptionIndex != SelectedOptionIndex;

        private FpsSettingsModel Model => _settingsModel.FpsSettingsModel;

        public event Action OnChanged;

        public void Initialize()
        {
            SelectedOptionIndex = Model.SelectedOptionIndex;
        }

        public void ApplySettings()
        {
            if (HasChanged)
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
            if (!HasChanged)
                return;

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