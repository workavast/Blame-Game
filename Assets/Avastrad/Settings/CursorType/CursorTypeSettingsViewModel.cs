using System;

namespace Avastrad.Settings.CursorType
{
    public class CursorTypeSettingsViewModel : SettingViewModel<CursorTypeSettingsModel>
    {
        public override bool HasChanged => CustomCursor != _model.CustomCursor;
        public bool CustomCursor { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
            => LoadModelData();

        public override void SetToModel() 
            => _model.SetValue(CustomCursor);

        public override void ResetSetting() 
            => SetValue(_model.CustomCursor, true);

        public override void LoadModelData()
            => SetValue(_model.CustomCursor, true);

        public void SetValue(bool customCursor, bool notify)
        {
            CustomCursor = customCursor;
            _model.SetValueTemporary(customCursor);
            
            if (notify)
                OnChanged?.Invoke();
        }
    }
}