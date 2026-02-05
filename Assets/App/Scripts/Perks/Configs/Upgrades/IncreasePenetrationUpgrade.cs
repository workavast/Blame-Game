using App.Ecs.Bullets;
using App.EcsBridges;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreasePenetrationUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private int additionalPenetration;
        
        protected override object[] GetDescriptionParams()
            => new object[] { additionalPenetration };
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsBridge.GetComponentOfSingletonRO<TTag, BulletPenetration>();
            currentScale.Value += additionalPenetration;
            
            EcsBridge.TrySetComponentOfSingleton<TTag, BulletPenetration>(currentScale);
        }
    }
}