using App.Ecs.PlayerPerks.ForwardShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ForwardShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseForwardShooterDamage), menuName = PerksConsts.ForwardShooterPath + nameof(IncreaseForwardShooterDamage))]
    public class IncreaseForwardShooterDamage : IncreaseDamageScaleUpgrade<ForwardShooterTag>
    {
        
    }
}