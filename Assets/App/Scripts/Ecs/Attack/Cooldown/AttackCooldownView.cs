using System;
using App.Ecs.EntityViews;
using UnityEngine;

namespace App.Ecs.Attack.Cooldown
{
    public class AttackCooldownView : MonoBehaviour, IEntityViewElement
    {
        public event Action<float> OnCooldownPercentageUpdate; 
        
        public event Action<IEntityViewElement> OnCleanupCompleted;
        
        public bool OnDestroyCallback() 
            => true;

        public void UpdateCooldownPercentage(float percentage) 
            => OnCooldownPercentageUpdate?.Invoke(1 - percentage);
    }
}