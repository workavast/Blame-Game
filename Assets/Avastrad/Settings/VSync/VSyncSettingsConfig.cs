using UnityEngine;

namespace Avastrad.Settings.VSync
{
    [CreateAssetMenu(fileName = nameof(VSyncSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(VSyncSettingsConfig))]
    public class VSyncSettingsConfig : SettingsConfig
    {
        [field: SerializeField] public bool DefaultValue { get; private set; }
    }
}