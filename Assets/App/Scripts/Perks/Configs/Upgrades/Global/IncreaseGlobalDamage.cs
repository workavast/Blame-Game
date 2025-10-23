using App.Ecs;
using App.Ecs.Player;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseGlobalDamage), menuName = PerkConst.GlobalPath + nameof(IncreaseGlobalDamage))]
    public class IncreaseGlobalDamage : IncreaseDamageScaleUpgrade<PlayerTag>
    {
        
    }
}