using System;
using UnityEngine;

namespace App.Settings.Template
{
    [Serializable]
    public class TemplateSettingsModel : ISettingsModel
    {
        [field: SerializeField] public float Value { get; private set; }

        public int Priority => _config.Priority;
        public float DefaultValue => _config.DefaultValue;
        
        private readonly TemplateSettingsConfig _config;
        
        public TemplateSettingsModel(TemplateSettingsConfig config)
        {
            _config = config;
            
            Value = DefaultValue;
        }

        public void SetValue(float value)
        {
            Value = value;
        }
        
        public void Apply()
        {
            //apply value to the some system, if it required
        }

        public void ResetToDefault()
        {
            Value = DefaultValue;
        }

        public void Load(TemplateSettingsModel model)
        {
            Value = model.Value;
        }
    }
}