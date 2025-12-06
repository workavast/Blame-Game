using System;
using UnityEngine;

namespace Avastrad.Settings.Volume
{
    public class VolumeSettingViewModel : SettingViewModel<VolumeSettingModel>
    {
        public override bool HasChanged
            => !Mathf.Approximately(_model.MasterVolume, MasterVolume) ||
               !Mathf.Approximately(_model.MusicVolume, MusicVolume) ||
               !Mathf.Approximately(_model.EffectsVolume, EffectsVolume);

        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        public event Action OnChanged;

        protected override void Initialize()
        {
            MasterVolume = _model.MasterVolume;
            MusicVolume = _model.MusicVolume;
            EffectsVolume = _model.EffectsVolume;
        }

        public override void SetToModel()
        {
            _model.SetVolume(SettingsVolumeType.Master, MasterVolume);
            _model.SetVolume(SettingsVolumeType.Effects, EffectsVolume);
            _model.SetVolume(SettingsVolumeType.Music, MusicVolume);
        }

        public override void ResetSetting()
        {
            MasterVolume = _model.MasterVolume; 
            EffectsVolume = _model.EffectsVolume;
            MusicVolume = _model.MusicVolume;
            OnChanged?.Invoke();
        }

        public override void LoadModelData()
        {
            MasterVolume = _model.MasterVolume; 
            EffectsVolume = _model.EffectsVolume;
            MusicVolume = _model.MusicVolume;
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

            _model.SetVolumeTemporary(volumeType, value);
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