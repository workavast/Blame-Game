using App.Ecs.PlayerPerks.TurretsSpawner;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Turrets
{
    [CreateAssetMenu(fileName = nameof(IncreaseTurretsSpawnRate), menuName = PerksConsts.TurretsPath + nameof(IncreaseTurretsSpawnRate))]
    public class IncreaseTurretsSpawnRate : IncreaseFireRateUpgrade<TurretsSpawnerTag>
    {
        
    }
}