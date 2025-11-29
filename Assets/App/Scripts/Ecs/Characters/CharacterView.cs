using System;
using App.Ecs.EntityViews;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.Characters
{
    public class CharacterView : MonoBehaviour, IEntityViewElement
    {
        public float Velocity { get; private set; }
        public event Action<IEntityViewElement> OnCleanupCompleted;

        public bool OnDestroyCallback() 
            => true;

        public void SetVelocity(float3 velocity) 
            => Velocity = ((Vector3)velocity).magnitude;

        public void SetVelocity(float velocity) 
            => Velocity = velocity;

        public void SetPositionAndRotation(float3 position, quaternion rotation) 
            => transform.SetPositionAndRotation(position, rotation);
        
        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation)
            => transform.rotation = rotation;
    }
}