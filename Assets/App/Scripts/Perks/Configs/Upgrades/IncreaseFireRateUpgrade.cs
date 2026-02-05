using App.Ecs.Attack;
using App.Ecs.Attack.Cooldown;
using App.EcsBridges;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseFireRateUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float additionalFireRate;
        
        protected override object[] GetDescriptionParams()
            => new object[] { additionalFireRate };
        
        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsBridge.GetComponentOfSingletonRO<TTag, AttackRateScale>();
            currentScale.Value += additionalFireRate;
            
            EcsBridge.TrySetComponentOfSingleton<TTag, AttackRateScale>(currentScale);
        }
    }
}