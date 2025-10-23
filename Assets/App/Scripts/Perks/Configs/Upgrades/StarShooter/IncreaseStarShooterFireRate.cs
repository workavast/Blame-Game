using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.StarShooter;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.StarShooter
{
    [CreateAssetMenu(fileName = nameof(IncreaseStarShooterFireRate), menuName = PerkConst.StarShooterPath + nameof(IncreaseStarShooterFireRate))]
    public class IncreaseStarShooterFireRate : IncreaseFireRateUpgrade<StarShooterTag>
    {
        
    }
}