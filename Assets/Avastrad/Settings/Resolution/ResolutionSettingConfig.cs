using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    [CreateAssetMenu(fileName = nameof(ResolutionSettingConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(ResolutionSettingConfig))]
    public class ResolutionSettingConfig : SettingConfig
    {
        [Space]
        [SerializeField] private List<InspectorResolution> resolutions;
        
        public IReadOnlyList<InspectorResolution> Resolutions => resolutions;
        
        public override ISettingModel CreateModel() 
            => new ResolutionSettingModel(this);
    }
}