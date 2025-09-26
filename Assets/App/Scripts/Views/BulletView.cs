using Unity.Entities.Content;
using Unity.Mathematics;
using UnityEngine;

namespace App.Views
{
    public class BulletView : CleanupView
    {
        private WeakObjectReference<BulletView> _prefab;

        protected override void DestroyCallback()
        {
            Debug.Log($"{name} is destroyed");
            _prefab.Release();
            Destroy(gameObject);
        }

        public void SetPrefab(WeakObjectReference<BulletView> prefab) 
            => _prefab = prefab;

        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation) 
            => transform.rotation = rotation;
    }
}