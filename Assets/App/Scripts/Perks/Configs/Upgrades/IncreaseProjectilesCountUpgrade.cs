using App.Ecs;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseProjectilesCountUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private int additionalProjectilesCount;
        
        protected override object[] GetDescriptionParams()
            => new object[] { additionalProjectilesCount };
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentCount = EcsSingletons.GetComponentOfSingletonRO<TTag, AdditionalProjectilesCount>();
            currentCount.Value += additionalProjectilesCount;
            
            EcsSingletons.TrySetComponentOfSingleton<TTag, AdditionalProjectilesCount>(currentCount);
        }
    }
}