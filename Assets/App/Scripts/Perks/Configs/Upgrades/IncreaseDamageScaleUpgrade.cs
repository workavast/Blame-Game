using App.Ecs.Attack;
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
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<TTag, AttackDamageScale>();
            currentScale.Value += damageScale;
            
            EcsSingletons.TrySetComponentOfSingleton<TTag, AttackDamageScale>(currentScale);
        }
    }
}
