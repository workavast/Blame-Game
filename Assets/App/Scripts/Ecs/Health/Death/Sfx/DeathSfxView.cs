using System;
using App.Audio.Sources;
using App.Ecs.EntityViews;
using Unity.Entities.Content;
using UnityEngine;
using Zenject;

namespace App.Ecs.Health.Death.Sfx
{
    public class DeathSfxView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private Vector2 deathPitchRange;

        private SfxHolder _sfxHolder;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;

        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }
        
        protected void OnDestroy() 
            => _sfxHolder.ReleaseIfUnused();

        public bool OnDestroyCallback()
            => true;

        public void SetDeathSfx(WeakObjectReference<AudioPoolRelease> deathSfxRef) 
            => _sfxHolder.SetSfx(deathSfxRef);

        public void Activate() 
            => _sfxHolder.Play(transform.position, deathPitchRange);
    }
}