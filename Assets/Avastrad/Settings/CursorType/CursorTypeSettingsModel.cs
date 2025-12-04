using System;
using UnityEngine;

namespace Avastrad.Settings.CursorType
{
    public class CursorTypeSettingsModel : ISettingModel
    {
        public bool CustomCursor { get; private set; }

        public int Priority => _config.Priority;
        public bool DefaultValue => _config.UseCustomCursor;
        
        private readonly CursorTypeSettingsConfig _config;
        
        public CursorTypeSettingsModel(CursorTypeSettingsConfig config)
        {
            _config = config;
            
            CustomCursor = DefaultValue;
        }

        public void SetValue(bool customCursor)
        {
            CustomCursor = customCursor;
        }
        
        public void Apply()
        {
            if (CustomCursor)
                Cursor.SetCursor(_config.CustomCursor, _config.HotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        
        public void SetValueTemporary(bool customCursor)
        {
            if (customCursor)
                Cursor.SetCursor(_config.CustomCursor, _config.HotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        
        public void ResetToDefault() 
            => CustomCursor = DefaultValue;

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            CustomCursor = state.CustomCursor;
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