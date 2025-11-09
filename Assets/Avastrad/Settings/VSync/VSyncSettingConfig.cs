using UnityEngine;

namespace Avastrad.Settings.VSync
{
    [CreateAssetMenu(fileName = nameof(VSyncSettingConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(VSyncSettingConfig))]
    public class VSyncSettingConfig : SettingConfig
    {
        [field: SerializeField] public bool DefaultValue { get; private set; }
        
        public override ISettingModel CreateModel() 
            => new VSyncSettingModel(this);
    }
}