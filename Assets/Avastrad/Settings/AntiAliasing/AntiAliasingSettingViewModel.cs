using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    public class AntiAliasingSettingViewModel : SettingViewModel<AntiAliasingSettingModel>
    {
        public override bool HasChanged => _model.SelectedAntiAliasingIndex != SelectedAntiAliasingIndex;
        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => _model.AntiAliasingModes;
        public int SelectedAntiAliasingIndex { get; private set; }

        public event Action OnChanged;

        protected override void Initialize() 
            => LoadModelData();

        public override void SetToModel() 
            => _model.SetValue(SelectedAntiAliasingIndex);

        public override void ResetSetting()
            => SetValue(_model.SelectedAntiAliasingIndex, true);

        public override void LoadModelData() 
            => SetValue(_model.SelectedAntiAliasingIndex, true);

        public void SetValue(int antiAliasingIndex, bool notify)
        {
            SelectedAntiAliasingIndex = antiAliasingIndex;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}