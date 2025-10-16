using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.StarShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.StarShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseStarShooterBulletsCount), menuName = PerkConst.StarShooterPath + nameof(IncreaseStarShooterBulletsCount))]
    public class IncreaseStarShooterBulletsCount : IncreaseProjectilesCountUpgrade<StarShooterTag>
    {
        
    }
}