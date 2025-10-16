using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.StarShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.StarShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseStarShooterDamage), menuName = PerkConst.StarShooterPath + nameof(IncreaseStarShooterDamage))]
    public class IncreaseStarShooterDamage : IncreaseDamageScaleUpgrade<StarShooterTag>
    {
    }
}