using Unity.Entities.Content;
using Unity.Mathematics;
using UnityEngine;

namespace App
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private CleanupCallback cleanupCallback;

        public CleanupCallback CleanupCallback => cleanupCallback;
        
        public float Velocity { get; private set; }

        private WeakObjectReference<EntityView> _prefab;

        private void Awake()
        {
            cleanupCallback.SetCallback(DestroyCallback);
        }

        public void SetPrefab(WeakObjectReference<EntityView> prefab) 
            => _prefab = prefab;

        public void SetVelocity(float3 velocity) 
            => Velocity = ((Vector3)velocity).magnitude;

        public void SetVelocity(float velocity) 
            => Velocity = velocity;

        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation) 
            => transform.rotation = rotation;

        public void DestroyCallback()
        {
            Debug.Log($"{name} is destroyed");
            _prefab.Release();
        }
    }
}