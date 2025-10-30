using System;
using Avastrad.PoolSystem.LiteDynamic;
using UnityEngine;

namespace App.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolderPoolable : AudioSourceHolder, IPoolable<AudioSourceHolderPoolable>
    {
        public event Action<AudioSourceHolderPoolable> ReturnElementEvent;
        public event Action<AudioSourceHolderPoolable> DestroyElementEvent;

        private void Update()
        {
            if (!_audioSource.isPlaying) 
                ReturnElementEvent?.Invoke(this);
        }

        protected virtual void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
        
        public void OnElementExtractFromPool()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif
            _audioSource.Stop();
            _audioSource.Play();
        }

        public void OnElementReturnInPool()
        {
            _audioSource.Stop();
            
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }
    }
}