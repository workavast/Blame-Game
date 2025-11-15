using App.Ecs.Clenuping;
using App.Ecs.Death.Vfx;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.Characters
{
    public class CharacterView : CleanupView
    {
        [SerializeField] private GameObject model;
        [SerializeField] private DeathVfxView deathVfxView;
        
        public float Velocity { get; private set; }
        
        protected override void DestroyCallback()
        {
            if (deathVfxView != null && deathVfxView.IsPlay)
            {
                model.SetActive(false);
                deathVfxView.OnOver += () => Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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