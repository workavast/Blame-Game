using App.Ecs.PlayerPerks;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreasePenetrationUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private int additionalPenetration;
        
        public override void Perform(PerksManager perksManager)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AdditionalPenetration>();
            currentScale.Value += additionalPenetration;
            
            EcsSingletons.TrySetComponentOfSingletonRW<TTag, AdditionalPenetration>(currentScale);
        }
    }
}