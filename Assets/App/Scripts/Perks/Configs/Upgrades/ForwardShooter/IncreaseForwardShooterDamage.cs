using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.ForwardShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ForwardShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseForwardShooterDamage), menuName = PerkConst.ForwardShooterPath + nameof(IncreaseForwardShooterDamage))]
    public class IncreaseForwardShooterDamage : IncreaseDamageScaleUpgrade<ForwardShooterTag>
    {
        
    }
}