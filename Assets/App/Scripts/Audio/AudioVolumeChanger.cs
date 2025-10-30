using System;
using UnityEngine;
using UnityEngine.Audio;

namespace App.Audio
{
    public class AudioVolumeChanger
    {
        private readonly AudioMixer _mixer;
        private readonly string _masterParam;
        private readonly string _effectsParam;
        private readonly string _musicParam;

        public AudioVolumeChanger(AudioMixer mixer, string masterParam, string effectsParam, string musicParam)
        {
            _mixer = mixer;
            _masterParam = masterParam;
            _effectsParam = effectsParam;
            _musicParam = musicParam;
        }

        /// <summary>
        /// Dont forgot apply changes by <see cref="Apply"/>
        /// </summary>
        public void SetVolume(VolumeType volumeType, float newVolume)
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    SetVolume(_masterParam, newVolume);
                    break;
                case VolumeType.Effects:
                    SetVolume(_effectsParam, newVolume);
                    break;
                case VolumeType.Music:
                    SetVolume(_musicParam, newVolume);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }
        }

        private void SetVolume(string paramName, float newVolume)
            => _mixer.SetFloat($"{paramName}", Mathf.Lerp(-80, 0, Mathf.Sqrt(Mathf.Sqrt(newVolume))));
    }
}