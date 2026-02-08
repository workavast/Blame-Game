using App.Ecs.PlayerPerks.TurretsSpawner;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Turrets
{
    [CreateAssetMenu(fileName = nameof(IncreaseTurretsDamage), menuName = PerksConsts.TurretsPath + nameof(IncreaseTurretsDamage))]
    public class IncreaseTurretsDamage : IncreaseDamageScaleUpgrade<TurretsSpawnerTag>
    {
    }
}