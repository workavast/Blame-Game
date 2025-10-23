using App.Ecs;
using App.Ecs.PlayerPerks;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseFireRateUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float additionalFireRate;
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AttackRateScale>();
            currentScale.Value += additionalFireRate;
            
            EcsSingletons.TrySetComponentOfSingleton<TTag, AttackRateScale>(currentScale);
        }
    }
}