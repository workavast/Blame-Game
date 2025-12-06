using UnityEngine;

namespace Avastrad.Settings.CameraShakePower
{
    [CreateAssetMenu(fileName = nameof(CameraShakePowerSettingConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(CameraShakePowerSettingConfig))]
    public class CameraShakePowerSettingConfig : SettingConfig
    {
        [field: SerializeField, Range(0, 1)] public float DefaultShakePower { get; private set; }
        
        public override ISettingModel CreateModel() 
            => new CameraShakePowerSettingModel(this);
    }
}