using App.Ecs.Player;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseGlobalDamage), menuName = PerksConsts.GlobalPath + nameof(IncreaseGlobalDamage))]
    public class IncreaseGlobalDamage : IncreaseDamageScaleUpgrade<PlayerTag>
    {
        
    }
}