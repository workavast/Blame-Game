using App.Ecs;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseDamageScaleUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float damageScale;
        
        public override void Perform(PerksManager perksManager)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, DamageScale>();
            currentScale.Value += damageScale;
            
            EcsSingletons.TrySetComponentOfSingletonRW<TTag, DamageScale>(currentScale);
        }
    }
}
