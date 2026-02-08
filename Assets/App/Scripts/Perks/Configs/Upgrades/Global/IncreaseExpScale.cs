using App.Ecs.Experience;
using App.Ecs.Player;
using App.EcsBridges;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseExpScale), menuName = PerksConsts.GlobalPath + nameof(IncreaseExpScale))]
    public class IncreaseExpScale : PerformPerk
    {
        [SerializeField] private float scale;

        protected override object[] GetDescriptionParams() 
            => new object[] { scale };

        public override void Perform(PerksActivator perksActivator)
        {
            var currentScale = EcsBridge.GetComponentOfSingletonRO<PlayerTag, ExpScale>();
            currentScale.Value += scale;
            
            EcsBridge.TrySetComponentOfSingleton<PlayerTag, ExpScale>(currentScale);
        }
    }
}