using System;
using App.Ecs.EntityViews;
using UnityEngine;

namespace App.Ecs.Attack.Vfx
{
    public class AttackVfxView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private ParticleProvider attackVfx;

        public event Action<IEntityViewElement> OnCleanupCompleted;

        public bool OnDestroyCallback() 
            => true;
        
        public void PerformAttack()
            => attackVfx.Play();
    }
}