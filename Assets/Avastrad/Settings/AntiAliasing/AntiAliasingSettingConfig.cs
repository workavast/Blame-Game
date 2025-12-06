using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    [CreateAssetMenu(fileName = nameof(AntiAliasingSettingConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(AntiAliasingSettingConfig))]
    public class AntiAliasingSettingConfig : SettingConfig
    {
        [SerializeField] private List<AntialiasingMode> antiAliasingModes;
        [field: SerializeField] public int DefaultAntiAliasingIndex { get; private set; }

        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => antiAliasingModes;
        
        public override ISettingModel CreateModel() 
            => new AntiAliasingSettingModel(this);
    }
}