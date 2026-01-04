using Avastrad.Settings;
using Avastrad.Settings.Volume;
using UnityEngine;

namespace App.Audio.Settings
{
    public class SettingsAudioApplier
    {
        private readonly VolumeSettingModel _volumeSettingModel;
        private readonly AudioVolumeChanger _audioVolumeChanger;
        
        public SettingsAudioApplier(SettingsRepository settingsRepository, VolumeSettingConfig config)
        {
            _volumeSettingModel = settingsRepository.GetSettingModel<VolumeSettingModel>();
            _audioVolumeChanger = new AudioVolumeChanger(config.AudioMixer, config.MasterParam, config.EffectsParam,
                config.MusicParam);
            
            _volumeSettingModel.OnApply += ApplySettings;
            _volumeSettingModel.OnTempApply += ApplyTempValues;
        }

        private void ApplyTempValues(SettingsVolumeType settingsVolumeType, float volume)
        {
            _audioVolumeChanger.SetVolume(settingsVolumeType.ToVolumeType(), volume);
        }

        private void ApplySettings()
        {
            _audioVolumeChanger.SetVolume(VolumeType.Master, _volumeSettingModel.MasterVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Effects, _volumeSettingModel.EffectsVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Music, _volumeSettingModel.MusicVolume);
        }
    }
}