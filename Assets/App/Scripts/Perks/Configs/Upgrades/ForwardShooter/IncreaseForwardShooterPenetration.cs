using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ForwardShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseForwardShooterPenetration), menuName = PerkConst.ForwardShooterPath + nameof(IncreaseForwardShooterPenetration))]
    public class IncreaseForwardShooterPenetration : IncreasePenetrationUpgrade<ForwardShooterTag>
    {
        
    }
}