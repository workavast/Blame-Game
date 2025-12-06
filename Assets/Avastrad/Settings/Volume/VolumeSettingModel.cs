using System;

namespace Avastrad.Settings.Volume
{
    public class VolumeSettingModel : ISettingModel
    {
        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }
        public bool HasChanged { get; private set; }

        public int Priority => _config.Priority;
        public float DefaultMasterVolume => _config.DefaultMasterVolume;
        public float DefaultMusicVolume => _config.DefaultMusicVolume;
        public float DefaultEffectsVolume => _config.DefaultEffectsVolume;
        
        private readonly VolumeSettingConfig _config;
        public event Action OnApply;
        public event Action<SettingsVolumeType, float> OnTempApply;
        
        public VolumeSettingModel(VolumeSettingConfig config)
        {
            _config = config;
            
            MasterVolume = DefaultMasterVolume;
            MusicVolume = DefaultMusicVolume;
            EffectsVolume = DefaultEffectsVolume;
        }
    
        public void SetVolume(SettingsVolumeType volumeType, float value)
        {
            switch (volumeType)
            {
                case SettingsVolumeType.Master:
                    MasterVolume = value;
                    break;
                case SettingsVolumeType.Effects:
                    EffectsVolume = value;
                    break;
                case SettingsVolumeType.Music:
                    MusicVolume = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }

            HasChanged = true;
        }
        
        public void SetVolumeTemporary(SettingsVolumeType volumeType, float value)
        {
            HasChanged = true;
            OnTempApply?.Invoke(volumeType, value);
        }

        public void Apply()
        {
            HasChanged = false;
            OnApply?.Invoke();
        }

        public void ResetToDefault()
        {
            MasterVolume = DefaultMasterVolume;
            MusicVolume = DefaultMusicVolume;
            EffectsVolume = DefaultEffectsVolume;
            HasChanged = true;
        }
        
        public Type GetStateType() 
            => typeof(SettingState);
        
        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            MasterVolume = state.MasterVolume;
            MusicVolume = state.MusicVolume;
            EffectsVolume = state.EffectsVolume;
            HasChanged = true;
        }
        
        private struct SettingState : ISettingState
        {
            public float MasterVolume { get; set; }
            public float MusicVolume { get; set; }
            public float EffectsVolume { get; set; }

            public SettingState(VolumeSettingModel model)
            {
                MasterVolume = model.MasterVolume;
                MusicVolume = model.MusicVolume;
                EffectsVolume = model.EffectsVolume;
            }
        }
    }
}