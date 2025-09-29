using Unity.Mathematics;
using UnityEngine;

namespace App.Views
{
    public class RocketView : CleanupView
    {
        [SerializeField] private Transform explosionSphere;
        
        private float _explosionRadius;

        protected override void DestroyCallback()
        {
            explosionSphere.gameObject.SetActive(true);
            explosionSphere.localScale = Vector3.one * _explosionRadius;
            
            Invoke(nameof(DestroyInternal), 0.5f);
        }

        private void DestroyInternal() 
            => base.DestroyCallback();

        public void SetPosition(float3 position)
            => transform.position = position;

        public void SetExplosionRadius(float explosionRadius)
            => _explosionRadius = explosionRadius;
    }
}