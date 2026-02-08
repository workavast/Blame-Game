using App.Ecs.PlayerPerks.ForwardShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ForwardShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseForwardShooterFireRate), menuName = PerksConsts.ForwardShooterPath + nameof(IncreaseForwardShooterFireRate))]
    public class IncreaseForwardShooterFireRate : IncreaseFireRateUpgrade<ForwardShooterTag>
    {
        
    }
}