using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.StarShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseStarShooterBulletsCount), menuName = PerkConst.StarShooterPath + nameof(IncreaseStarShooterBulletsCount))]
    public class IncreaseStarShooterBulletsCount : IncreaseProjectilesCountUpgrade<StarShooterTag>
    {
        
    }
}