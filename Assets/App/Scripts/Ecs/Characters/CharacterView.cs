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
        [SerializeField] private GameObject model;
        [SerializeField] private Vector2 deathPitchRange;
        [SerializeField] private ParticleProvider particleProvider;
        
        private SfxHolder _sfxHolder;
        
        public float Velocity { get; private set; }

        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }

        protected override void Awake()
        {
            base.Awake();

            if (particleProvider != null)
                particleProvider.OnStopped += () => Destroy(gameObject);;
        }

        protected override void OnDestroy()
        {
            _sfxHolder.ReleaseIfUnused();
            base.OnDestroy();
        }

        protected override void DestroyCallback()
        {
            _sfxHolder.Play(transform.position, deathPitchRange);

            
            if (particleProvider == null)
                Destroy(gameObject);
            else
            {
                model.SetActive(false);
                particleProvider.Play();
            }
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