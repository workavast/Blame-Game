using App.Ecs.Clenuping;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.Characters
{
    public class CharacterView : CleanupView
    {
        public float Velocity { get; private set; }

        protected override void DestroyCallback()
        {
            Debug.Log($"{name} is destroyed");
            _prefab.Release();
            Destroy(gameObject);
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