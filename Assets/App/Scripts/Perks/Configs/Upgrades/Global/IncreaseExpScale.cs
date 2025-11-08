using App.Ecs.Experience;
using App.Ecs.Player;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseExpScale), menuName = PerkConst.GlobalPath + nameof(IncreaseExpScale))]
    public class IncreaseExpScale : PerformPerk
    {
        [SerializeField] private float scale;

        protected override object[] GetDescriptionParams() 
            => new object[] { scale };

        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<PlayerTag, ExpScale>();
            currentScale.Value += scale;
            
            EcsSingletons.TrySetComponentOfSingleton<PlayerTag, ExpScale>(currentScale);
        }
    }
}