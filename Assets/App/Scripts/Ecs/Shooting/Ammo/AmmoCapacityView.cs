using System;
using App.Ecs.EntityViews;
using UnityEngine;

namespace App.Ecs.Shooting.Ammo
{
    public class AmmoCapacityView : MonoBehaviour, IEntityViewElement
    {
        public event Action<float> OnCapacityPercentageChanged;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;

        public bool OnDestroyCallback() 
            => true;

        public void SetCapacityPercentage(float percentage) 
            => OnCapacityPercentageChanged?.Invoke(percentage);
    }
}