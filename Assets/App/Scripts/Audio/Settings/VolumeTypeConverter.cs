using System;
using Avastrad.Settings.Volume;

namespace App.Audio.Settings
{
    public static class VolumeTypeConverter
    {
        public static VolumeType ToVolumeType(this SettingsVolumeType settingsVolumeType)
        {
            return settingsVolumeType switch
            {
                SettingsVolumeType.Master => VolumeType.Master,
                SettingsVolumeType.Effects => VolumeType.Effects,
                SettingsVolumeType.Music => VolumeType.Music,
                _ => throw new ArgumentOutOfRangeException(nameof(settingsVolumeType), settingsVolumeType, null)
            };
        }
    }
}