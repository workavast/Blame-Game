using System;
using UnityEngine;
using Zenject;

namespace Avastrad.Settings.Volume
{
    public class VolumeSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private VolumeSettingsModel Model => _settingsModel.VolumeSettingsModel;

        public bool HasChanged
            => !Mathf.Approximately(Model.MasterVolume, MasterVolume) ||
               !Mathf.Approximately(Model.MusicVolume, MusicVolume) ||
               !Mathf.Approximately(Model.EffectsVolume, EffectsVolume);

        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        public Action OnChanged;
        
        public void Initialize()
        {
            MasterVolume = Model.MasterVolume;
            MusicVolume = Model.MusicVolume;
            EffectsVolume = Model.EffectsVolume;
        }

        public void ApplySettings()
        {
            Model.SetVolume(SettingsVolumeType.Master, MasterVolume);
            Model.SetVolume(SettingsVolumeType.Effects, EffectsVolume);
            Model.SetVolume(SettingsVolumeType.Music, MusicVolume);
        }

        public void ResetSettings()
        {
            MasterVolume = Model.MasterVolume; 
            EffectsVolume = Model.EffectsVolume;
            MusicVolume = Model.MusicVolume;
            ApplySettings();
            Model.Apply();
            
            OnChanged?.Invoke();
        }

        public void ResetToDefault()
        {
            MasterVolume = Model.DefaultMasterVolume; 
            EffectsVolume = Model.DefaultEffectsVolume;
            MusicVolume = Model.DefaultMusicVolume;
            ApplySettings();
            
            OnChanged?.Invoke();
        }

        public void Set(SettingsVolumeType volumeType, float value, bool notify)
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

            Model.SetVolumeTemporary(volumeType, value);
            if (notify)
                OnChanged?.Invoke();
        }

        public float GetVolume(SettingsVolumeType volumeType)
        {
            switch (volumeType)
            {
                case SettingsVolumeType.Master:
                    return MasterVolume;
                case SettingsVolumeType.Effects:
                    return EffectsVolume;
                case SettingsVolumeType.Music:
                    return MusicVolume;
                default:
                    throw new ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }
        }
    }
}