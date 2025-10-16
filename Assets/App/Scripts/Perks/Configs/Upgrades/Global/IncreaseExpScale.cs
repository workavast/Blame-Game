using App.Ecs;
using App.Ecs.Experience;
using App.Perks.PerksManagement;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseExpScale), menuName = PerkConst.GlobalPath + nameof(IncreaseExpScale))]
    public class IncreaseExpScale : PerformPerk
    {
        [SerializeField] private float scale;
        
        public override void Perform(PerksManager perksManager)
        {
            var currentScale = EcsSingletons.GetComponentOfSingletonRO<PlayerTag, ExpScale>();
            currentScale.Value += scale;
            
            EcsSingletons.TrySetComponentOfSingletonRW<PlayerTag, ExpScale>(currentScale);
        }
    }
}