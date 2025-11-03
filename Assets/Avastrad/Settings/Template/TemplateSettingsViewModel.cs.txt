using System;
using UnityEngine;
using Zenject;

namespace App.Settings.Template
{
    public class TemplateSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private TemplateSettingsModel Model => new(null);//in real case use => _settingsModel.TemplateSettingsModel;
        
        public float Value { get; private set; }

        public Action OnChanged;
        
        public void Initialize()
        {
            Value = Model.Value;
        }

        public void ApplySettings()
        {
            Model.SetValue(Value);
        }

        public void ResetSettings()
        {
            Value = Model.Value;
            
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            Value = Model.DefaultValue;

            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void SetValue(float value, bool notify)
        {
            Value = value;

            if (notify)
                OnChanged?.Invoke();
        }
    }
}