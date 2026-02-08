using App.Ecs.PlayerPerks.ForwardShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ForwardShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseForwardShooterPenetration), menuName = PerksConsts.ForwardShooterPath + nameof(IncreaseForwardShooterPenetration))]
    public class IncreaseForwardShooterPenetration : IncreasePenetrationUpgrade<ForwardShooterTag>
    {
        
    }
}