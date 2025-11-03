using Avastrad.Settings;
using Avastrad.Settings.Volume;
using UnityEngine;

namespace App.Audio.Settings
{
    public class SettingsAudioApplier
    {
        private readonly VolumeSettingsModel _volumeSettingsModel;
        private readonly AudioVolumeChanger _audioVolumeChanger;
        
        public SettingsAudioApplier(SettingsModel settingsModel, VolumeSettingsConfig config)
        {
            _volumeSettingsModel = settingsModel.VolumeSettingsModel;
            _audioVolumeChanger = new AudioVolumeChanger(config.AudioMixer, config.MasterParam, config.EffectsParam,
                config.MusicParam);
            
            _volumeSettingsModel.OnApply += ApplySettings;
            _volumeSettingsModel.OnTempApply += ApplyTempValues;
        }

        private void ApplyTempValues(SettingsVolumeType settingsVolumeType, float volume)
        {
            _audioVolumeChanger.SetVolume(settingsVolumeType.ToVolumeType(), volume);
        }

        private void ApplySettings()
        {
            _audioVolumeChanger.SetVolume(VolumeType.Master, _volumeSettingsModel.MasterVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Effects, _volumeSettingsModel.EffectsVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Music, _volumeSettingsModel.MusicVolume);
        }
    }
}