using Unity.Mathematics;
using UnityEngine;

namespace App.Views
{
    public class RocketView : CleanupView
    {
        [SerializeField] private GameObject rocketModelHolder;
        [SerializeField] private Transform explosionSphere;
        [SerializeField] private ParticleCallbackProvider particleCallbackProvider;
        
        private float _explosionRadius;

        protected override void Awake()
        {
            base.Awake();
            
            particleCallbackProvider.OnStopped += DestroyInternal;
        }

        protected override void DestroyCallback()
        {
            explosionSphere.localScale = Vector3.one * _explosionRadius;
            explosionSphere.gameObject.SetActive(true);
            rocketModelHolder.SetActive(false);
        }

        private void DestroyInternal() 
            => base.DestroyCallback();

        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetExplosionRadius(float explosionRadius)
            => _explosionRadius = explosionRadius;
    }
}