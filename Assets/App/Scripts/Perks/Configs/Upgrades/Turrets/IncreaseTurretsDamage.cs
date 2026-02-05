using App.Ecs.PlayerPerks.TurretsSpawner;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Turrets
{
    [CreateAssetMenu(fileName = nameof(IncreaseTurretsDamage), menuName = PerkConst.TurretsPath + nameof(IncreaseTurretsDamage))]
    public class IncreaseTurretsDamage : IncreaseDamageScaleUpgrade<TurretsSpawnerTag>
    {
    }
}