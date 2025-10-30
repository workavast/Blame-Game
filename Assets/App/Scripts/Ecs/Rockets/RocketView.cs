using App.Audio;
using App.Audio.Sources;
using App.Ecs.Clenuping;
using Unity.Entities.Content;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace App.Ecs.Rockets
{
    public class RocketView : CleanupView
    {
        [SerializeField] private GameObject rocketModelHolder;
        [SerializeField] private Transform explosionSphere;
        [SerializeField] private ParticleCallbackProvider particleCallbackProvider;
        [Header("SFX")]
        [SerializeField] private Vector2 explosionPitchRange;

        private float _explosionRadius;

        private SfxHolder _sfxHolder;
        
        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            particleCallbackProvider.OnStopped += DestroyInternal;
        }

        protected override void OnDestroy()
        {
            _sfxHolder.ReleaseIfUnused();
            base.OnDestroy();
        }

        protected override void DestroyCallback()
        {
            explosionSphere.localScale = Vector3.one * _explosionRadius;
            explosionSphere.gameObject.SetActive(true);
            rocketModelHolder.SetActive(false);

            _sfxHolder.Play(transform.position, explosionPitchRange);
        }

        private void DestroyInternal() 
            => base.DestroyCallback();

        public void SetSfxView(WeakObjectReference<AudioPoolRelease> sfxRef)
        {
            _sfxHolder.SetSfx(sfxRef);
        }
        
        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetExplosionRadius(float explosionRadius)
            => _explosionRadius = explosionRadius;
    }
}