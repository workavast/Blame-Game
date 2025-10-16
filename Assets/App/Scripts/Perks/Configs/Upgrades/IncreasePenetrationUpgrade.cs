using App.Ecs;
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
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AdditionalPenetration>();
            currentScale.Value += additionalPenetration;
            
            EcsSingletons.TrySetComponentOfSingleton<TTag, AdditionalPenetration>(currentScale);
        }
    }
}