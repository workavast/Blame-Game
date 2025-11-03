using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    [CreateAssetMenu(fileName = nameof(CameraShakePowerSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(CameraShakePowerSettingsConfig))]
    public class CameraShakePowerSettingsConfig : SettingsConfig
    {
        [field: SerializeField, Range(0, 1)] public float DefaultShakePower { get; private set; }
    }
}