using System;

namespace Avastrad.Settings.CursorType
{
    public class CursorTypeSettingsViewModel : SettingViewModel<CursorTypeSettingsModel>
    {
        public override bool HasChanged => CustomCursor != _model.CustomCursor;
        public bool CustomCursor { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
        {
            CustomCursor = _model.CustomCursor;
        }

        public override void ApplySettings()
        {
            _model.SetValue(CustomCursor);
        }

        public override void ResetSettings()
        {
            CustomCursor = _model.CustomCursor;
            
            ApplySettings();
            _model.Apply();
            
            OnChanged?.Invoke();
        }

        public override void ResetToDefault()
        {
            _model.ResetToDefault();
            CustomCursor = _model.DefaultValue;
            
            OnChanged?.Invoke();
        }

        public void SetValue(bool customCursor, bool notify)
        {
            CustomCursor = customCursor;
            _model.SetValueTemporary(customCursor);
            
            if (notify)
                OnChanged?.Invoke();
        }
    }
}