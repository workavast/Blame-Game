using System;
using UnityEngine;

namespace Avastrad.Settings.Volume
{
    [Serializable]
    public class VolumeSettingsModel : ISettingsModel
    {
        [field: SerializeField] public float MasterVolume { get; private set; }
        [field: SerializeField] public float MusicVolume { get; private set; }
        [field: SerializeField] public float EffectsVolume { get; private set; }

        public int Priority => _config.Priority;
        public float DefaultMasterVolume => _config.DefaultMasterVolume;
        public float DefaultMusicVolume => _config.DefaultMusicVolume;
        public float DefaultEffectsVolume => _config.DefaultEffectsVolume;
        
        private readonly VolumeSettingsConfig _config;
        public event Action OnApply;
        public event Action<SettingsVolumeType, float> OnTempApply;
        
        public VolumeSettingsModel(VolumeSettingsConfig config)
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

        public void Load(VolumeSettingsModel model)
        {
            MasterVolume = model.MasterVolume;
            MusicVolume = model.MusicVolume;
            EffectsVolume = model.EffectsVolume;
        }
    }
}