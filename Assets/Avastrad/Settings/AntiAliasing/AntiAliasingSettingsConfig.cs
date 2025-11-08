using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Avastrad.Settings.AntiAliasing
{
    [CreateAssetMenu(fileName = nameof(AntiAliasingSettingsConfig),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(AntiAliasingSettingsConfig))]
    public class AntiAliasingSettingsConfig : SettingsConfig
    {
        [SerializeField] private List<AntialiasingMode> antiAliasingModes;
        [field: SerializeField] public int DefaultAntiAliasingIndex { get; private set; }

        public IReadOnlyList<AntialiasingMode> AntiAliasingModes => antiAliasingModes;
    }
}