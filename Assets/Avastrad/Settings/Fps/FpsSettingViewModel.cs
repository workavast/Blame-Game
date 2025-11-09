using System;
using System.Collections.Generic;

namespace Avastrad.Settings.Fps
{
    public class FpsSettingViewModel : SettingViewModel<FpsSettingModel>
    {
        public int SelectedOptionIndex { get; private set; }
        public IReadOnlyList<int> FpsOptions => _model.FpsOptions;
        public int DefaultOptionIndex => _model.DefaultOptionIndex;
        public override bool HasChanged => _model.SelectedOptionIndex != SelectedOptionIndex;

        public event Action OnChanged;

        protected override void Initialize()
        {
            SelectedOptionIndex = _model.SelectedOptionIndex;
        }

        public override void ApplySettings()
        {
            if (HasChanged)
                _model.Set(SelectedOptionIndex);
        }

        public void Set(int optionIndex, bool notify)
        {
            SelectedOptionIndex = optionIndex;
            if (notify)
                OnChanged?.Invoke();
        }

        public override void ResetSettings()
        {
            if (!HasChanged)
                return;

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
    }
}