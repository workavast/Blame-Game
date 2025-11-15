using System;
using UnityEngine;

namespace App.Ecs.Death
{
    public class DeathVfxView : MonoBehaviour
    {
        [SerializeField] private ParticleProvider deathVfx;

        public bool IsPlay => deathVfx.IsPlay;

        public event Action OnOver;

        private void Awake()
        {
            deathVfx.OnStopped += () => OnOver?.Invoke();
        }
        
        public void Activate()
        {
            deathVfx.Play();
        }
    }
}