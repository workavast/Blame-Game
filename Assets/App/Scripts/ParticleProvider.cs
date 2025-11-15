using System;
using UnityEngine;

namespace App
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleProvider : MonoBehaviour
    {
        public bool IsPlay => _particleSystem.isPlaying;
        
        private ParticleSystem _particleSystem;
        
        public event Action OnStopped;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            var particleMain = _particleSystem.main;
            particleMain.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Play() 
            => _particleSystem.Play();

        public void OnParticleSystemStopped() 
            => OnStopped?.Invoke();
    }
}