using UnityEngine;
using UnityEngine.Rendering;

namespace Avastrad.Settings.Brightness
{
    [CreateAssetMenu(fileName = nameof(BrightnessSettingConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(BrightnessSettingConfig))]
    public class BrightnessSettingConfig : SettingConfig
    {
        [field: SerializeField] public VolumeProfile DefaultVolume { get; private set; }
        [field: SerializeField] public float DefaultValue { get; private set; } = 0;
        [field: SerializeField] public float MinValue { get; private set; } = -1;
        [field: SerializeField] public float MaxValue { get; private set; } = 1;

        private void OnValidate()
        {
            DefaultValue = Mathf.Clamp(DefaultValue, MinValue, MaxValue);
        }
        
        public override ISettingModel CreateModel() 
            => new BrightnessSettingModel(this);
    }
}