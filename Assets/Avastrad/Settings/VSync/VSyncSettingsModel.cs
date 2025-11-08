using System;
using UnityEngine;

namespace Avastrad.Settings.VSync
{
    [Serializable]
    public class VSyncSettingsModel : ISettingsModel
    {
        [field: SerializeField] public bool UseVSync { get; private set; }

        public int Priority => _config.Priority;
        public bool DefaultValue => _config.DefaultValue;
        
        private readonly VSyncSettingsConfig _config;
        
        public VSyncSettingsModel(VSyncSettingsConfig config)
        {
            _config = config;
            
            UseVSync = DefaultValue;
        }

        public void SetValue(bool value)
        {
            UseVSync = value;
        }
        
        public void Apply()
        {
            QualitySettings.vSyncCount = UseVSync ? 1 : 0;
        }

        public void ResetToDefault()
        {
            UseVSync = DefaultValue;
        }

        public void Load(VSyncSettingsModel model)
        {
            UseVSync = model.UseVSync;
        }
    }
}