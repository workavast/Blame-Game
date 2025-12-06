using System;
using UnityEngine;

namespace Avastrad.Settings.CursorType
{
    public class CursorTypeSettingsModel : ISettingModel
    {
        public bool CustomCursor { get; private set; }
        public bool HasChanged { get; private set; }

        public int Priority => _config.Priority;
        private bool DefaultValue => _config.UseCustomCursor;
        
        private readonly CursorTypeSettingsConfig _config;
        
        public CursorTypeSettingsModel(CursorTypeSettingsConfig config)
        {
            _config = config;
            
            CustomCursor = DefaultValue;
        }

        public void SetValue(bool customCursor)
        {
            HasChanged = true;
            CustomCursor = customCursor;
        }

        public void Apply()
        {
            if (CustomCursor)
                Cursor.SetCursor(_config.CustomCursor, _config.HotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            HasChanged = false;
        }
        
        public void SetValueTemporary(bool customCursor)
        {
            if (customCursor)
                Cursor.SetCursor(_config.CustomCursor, _config.HotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            HasChanged = true;
        }
        
        public void ResetToDefault() 
            => SetValue(DefaultValue);

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            SetValue(state.CustomCursor);
        }

        private struct SettingState : ISettingState
        {
            public bool CustomCursor { get; set; }

            public SettingState(CursorTypeSettingsModel model)
            {
                CustomCursor = model.CustomCursor;
            }
        }
    }
}