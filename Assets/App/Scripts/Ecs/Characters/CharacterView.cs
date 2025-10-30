using App.Audio;
using App.Audio.Sources;
using App.Ecs.Clenuping;
using Unity.Entities.Content;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace App.Ecs.Characters
{
    public class CharacterView : CleanupView
    {
        [SerializeField] private Vector2 deathPitchRange;
        
        private SfxHolder _sfxHolder;
        
        public float Velocity { get; private set; }

        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }

        protected override void OnDestroy()
        {
            _sfxHolder.ReleaseIfUnused();
            base.OnDestroy();
        }

        protected override void DestroyCallback()
        {
            _sfxHolder.Play(transform.position, deathPitchRange);
            
            Destroy(gameObject);
        }

        public void SetDeathSfx(WeakObjectReference<AudioPoolRelease> deathSfxRef)
        {
            _sfxHolder.SetSfx(deathSfxRef);
        }
        
        public void SetVelocity(float3 velocity) 
            => Velocity = ((Vector3)velocity).magnitude;

        public void SetVelocity(float velocity) 
            => Velocity = velocity;

        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation) 
            => transform.rotation = rotation;
    }
}