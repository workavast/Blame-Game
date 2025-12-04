using UnityEngine;

namespace Avastrad.Settings.CursorType
{
    [CreateAssetMenu(fileName = nameof(CursorTypeSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(CursorTypeSettingsConfig))]
    public class CursorTypeSettingsConfig : SettingConfig
    {
        [field: SerializeField] public bool UseCustomCursor { get; private set; } = true;
        [field: SerializeField] public Texture2D CustomCursor { get; private set; }
        [field: SerializeField] public Vector2 HotSpot { get; private set; }
        
        public override ISettingModel CreateModel() 
            => new CursorTypeSettingsModel(this);
    }
}