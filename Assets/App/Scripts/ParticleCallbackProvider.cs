using System;
using UnityEngine;

namespace App
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleCallbackProvider : MonoBehaviour
    {
        public event Action OnStopped;

        private void Awake()
        {
            var particleMain = GetComponent<ParticleSystem>().main;
            particleMain.stopAction = ParticleSystemStopAction.Callback;
        }

        public void OnParticleSystemStopped() 
            => OnStopped?.Invoke();
    }
}