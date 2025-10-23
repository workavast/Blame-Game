using App.Ecs;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseAoeZoneRadius<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float additionalScale;
        
        public override void Perform(PerksManager perksManager)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AoeZoneRadiusScale>();
            currentScale.Value += additionalScale;
            
            EcsSingletons.TrySetComponentOfSingletonRW<TTag, AoeZoneRadiusScale>(currentScale);
        }
    }
}