using System;
using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    public class CameraShakePowerSettingViewModel : SettingViewModel<CameraShakePowerSettingModel>
    {
        public override bool HasChanged => !Mathf.Approximately(_model.ShakePower, ShakePower);
        public float ShakePower { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.SetValue(ShakePower);

        public override void ResetSetting() 
            => SetValue(_model.ShakePower, true);

        public override void LoadModelData()
            => SetValue(_model.ShakePower, true);

        public void SetValue(float shakePower, bool notify)
        {
            ShakePower = shakePower;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}