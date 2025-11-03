using UnityEngine;
using UnityEngine.Rendering;

namespace Avastrad.Settings.Brightness
{
    [CreateAssetMenu(fileName = nameof(BrightnessSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(BrightnessSettingsConfig))]
    public class BrightnessSettingsConfig : SettingsConfig
    {
        [field: SerializeField] public VolumeProfile DefaultVolume { get; private set; }
        [field: SerializeField] public float DefaultValue { get; private set; } = 0;
        [field: SerializeField] public float MinValue { get; private set; } = -1;
        [field: SerializeField] public float MaxValue { get; private set; } = 1;

        private void OnValidate()
        {
            DefaultValue = Mathf.Clamp(DefaultValue, MinValue, MaxValue);
        }
    }
}