using UnityEngine;

namespace Avastrad.Settings.ScreenMode
{
    [CreateAssetMenu(fileName = nameof(ScreenModeConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(ScreenModeConfig))]
    public class ScreenModeConfig : SettingConfig
    {
        [Header("Should be have priority less then resolution to worked")]
        [Space]
        [SerializeField] private bool defaultIsFullScreen;

        public bool DefaultIsFullScreen => defaultIsFullScreen;
        
        public override ISettingModel CreateModel() 
            => new ScreenModeSettingModel(this);
    }
}