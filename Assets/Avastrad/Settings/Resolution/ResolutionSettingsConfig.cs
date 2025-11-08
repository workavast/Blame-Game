using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    [CreateAssetMenu(fileName = nameof(ResolutionSettingsConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(ResolutionSettingsConfig))]
    public class ResolutionSettingsConfig : SettingsConfig
    {
        [Space]
        [SerializeField] private List<InspectorResolution> resolutions;
        
        public IReadOnlyList<InspectorResolution> Resolutions => resolutions;
    }
}