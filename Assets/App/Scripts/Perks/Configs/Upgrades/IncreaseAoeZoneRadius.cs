using App.Ecs;
using App.Ecs.AoeZones;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseAoeZoneRadius<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float additionalScale;
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AoeZoneRadiusScale>();
            currentScale.Value += additionalScale;
            
            EcsSingletons.TrySetComponentOfSingleton<TTag, AoeZoneRadiusScale>(currentScale);
        }
    }
}