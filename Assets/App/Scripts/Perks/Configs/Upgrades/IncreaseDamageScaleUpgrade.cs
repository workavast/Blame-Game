using App.Ecs.Attack;
using App.EcsBridges;
using App.Perks.PerksManagement;
using Unity.Entities;
using UnityEngine;

namespace App.Perks.Configs.Upgrades
{
    public abstract class IncreaseDamageScaleUpgrade<TTag> : PerformPerk
        where TTag : unmanaged, IComponentData
    {
        [SerializeField] private float damageScale;

        protected override object[] GetDescriptionParams()
            => new object[] { damageScale };

        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsBridge.GetComponentOfSingletonRO<TTag, AttackDamage>();
            currentScale.Scale += damageScale;
            
            EcsBridge.TrySetComponentOfSingleton<TTag, AttackDamage>(currentScale);
        }
    }
}
