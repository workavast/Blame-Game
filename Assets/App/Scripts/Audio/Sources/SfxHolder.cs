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
            Release();
            _sfxRef = sfxRef;
            _sfxRef.LoadAsync();
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

        public void Release()
        {
            if (_sfxRef.IsReferenceValid)
            {
                _sfxRef.TryRelease();
                _sfxRef = default;
            }
        }
    }
}