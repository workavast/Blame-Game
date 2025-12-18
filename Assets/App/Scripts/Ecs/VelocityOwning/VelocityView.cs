using System;
using App.Ecs.EntityViews;
using UnityEngine;

namespace App.Ecs.VelocityOwning
{
    public class VelocityView : MonoBehaviour, IEntityViewElement
    {
        public float Velocity { get; private set; }
        
        public event Action<IEntityViewElement> OnCleanupCompleted;
        
        public bool OnDestroyCallback() 
            => true;

        public void SetVelocity(float velocity) 
            => Velocity = velocity;
    }
}