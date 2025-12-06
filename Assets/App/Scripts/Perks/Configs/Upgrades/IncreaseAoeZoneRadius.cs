using App.Ecs.AoeZones;
using App.EcsBridges;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseAoeZoneRadius<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float additionalScale;
        
        protected override object[] GetDescriptionParams()
            => new object[] { additionalScale };
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsBridge.GetComponentOfSingletonRO<TTag, AoeZoneRadiusScale>();
            currentScale.Value += additionalScale;
            
            EcsBridge.TrySetComponentOfSingleton<TTag, AoeZoneRadiusScale>(currentScale);
        }
    }
}