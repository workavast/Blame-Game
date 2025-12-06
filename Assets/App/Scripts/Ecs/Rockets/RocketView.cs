using System;
using App.Audio.Sources;
using App.Ecs.EntityViews;
using Unity.Entities.Content;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace App.Ecs.Rockets
{
    public class RocketView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private GameObject rocketModelHolder;
        [SerializeField] private Transform explosionSphere;
        [SerializeField] private ParticleProvider particleProvider;
        [Header("SFX")]
        [SerializeField] private Vector2 explosionPitchRange;

        private float _explosionRadius;
        private SfxHolder _sfxHolder;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;
        
        [Inject]
        public void Construct(AudioFactory audioFactory)
        {
            _sfxHolder = new SfxHolder(audioFactory);
        }
        
        private void Awake() 
            => particleProvider.OnStopped += () => OnCleanupCompleted?.Invoke(this);

        public bool OnDestroyCallback()
        {
            explosionSphere.localScale = Vector3.one * _explosionRadius;
            explosionSphere.gameObject.SetActive(true);
            rocketModelHolder.SetActive(false);

            _sfxHolder.Play(transform.position, explosionPitchRange);

            return false;
        }
        
        private void OnDestroy() 
            => _sfxHolder.ReleaseIfUnused();
        
        public void SetSfxView(WeakObjectReference<AudioPoolRelease> sfxRef) 
            => _sfxHolder.SetSfx(sfxRef);

        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetExplosionRadius(float explosionRadius)
            => _explosionRadius = explosionRadius;
    }
}