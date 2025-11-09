using System;
using Newtonsoft.Json;

namespace Avastrad.Settings.Volume
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class VolumeSettingModel : ISettingModel
    {
        [JsonProperty] public float MasterVolume { get; private set; }
        [JsonProperty] public float MusicVolume { get; private set; }
        [JsonProperty] public float EffectsVolume { get; private set; }

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
        }
        
        public void SetVolumeTemporary(SettingsVolumeType volumeType, float value)
        {
            OnTempApply?.Invoke(volumeType, value);
        }

        public void Apply()
        {
            OnApply?.Invoke();
        }

        public void ResetToDefault()
        {
            MasterVolume = DefaultMasterVolume;
            MusicVolume = DefaultMusicVolume;
            EffectsVolume = DefaultEffectsVolume;
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