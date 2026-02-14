using App.Ecs.Shooting;
using App.Ecs.Shooting.Ammo;
using App.EcsBridges;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseCapacityUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private int additionalCapacity;

        protected override object[] GetDescriptionParams()
            => new object[] { additionalCapacity };

        public override void Perform(PerksActivator perksActivator)
        {
            var currentCapacity = EcsBridge.GetComponentOfSingletonRO<TTag, AmmoCapacity>();
            currentCapacity.DefaultValue += additionalCapacity;
            
            EcsBridge.TrySetComponentOfSingleton<TTag, AmmoCapacity>(currentCapacity);
        }
    }
}