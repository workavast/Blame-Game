using App.Utils;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Audio.Sources
{
    public class SfxHolder
    {
        private WeakObjectReference<AudioPoolRelease> _sfxRef;
        private readonly AudioFactory _audioFactory;
        
        public SfxHolder(AudioFactory audioFactory)
        {
            _audioFactory = audioFactory;
        }
        
        public void SetSfx(WeakObjectReference<AudioPoolRelease> sfxRef)
        {
            ReleaseIfUnused();

            _sfxRef = sfxRef;
        }

        public void Play(Vector3 position, Vector2 pitchRange)
        {
            if (!_sfxRef.IsReferenceValid)
                return;

            if (_sfxRef.LoadingStatus == ObjectLoadingStatus.Completed)
            {
                var source = _audioFactory.Create(_sfxRef.Result, position, pitchRange);
                source.SetAudioRef(_sfxRef);
            }
            else
                Debug.LogWarning("Sfx not loaded yet");
        }

        public void ReleaseIfUnused()
        {
            if (_sfxRef.IsReferenceValid) 
                _sfxRef.TryRelease();
        }
    }
}