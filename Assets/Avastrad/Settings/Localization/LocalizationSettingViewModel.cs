using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Avastrad.Settings.Localization
{
    public class LocalizationSettingViewModel : SettingViewModel<LocalizationSettingModel>
    {
        public override bool HasChanged => _model.SelectedOptionIndex != SelectedOptionIndex;
        public int SelectedOptionIndex { get; private set; }
        public IReadOnlyList<Locale> Options => _model.Options;

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.Set(SelectedOptionIndex);

        public override void ResetSetting() 
            => Set(_model.SelectedOptionIndex, true);

        public override void LoadModelData()
            => Set(_model.SelectedOptionIndex, true);

        public void Set(int optionIndex, bool notify)
        {
            SelectedOptionIndex = optionIndex;
            if (notify)
                OnChanged?.Invoke();
        }
    }
}