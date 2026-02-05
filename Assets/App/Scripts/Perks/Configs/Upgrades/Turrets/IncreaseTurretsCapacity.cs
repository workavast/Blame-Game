using App.Ecs.PlayerPerks.TurretsSpawner;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Turrets
{
    [CreateAssetMenu(fileName = nameof(IncreaseTurretsCapacity), menuName = PerkConst.TurretsPath + nameof(IncreaseTurretsCapacity))]
    public class IncreaseTurretsCapacity : IncreaseCapacityUpgrade<TurretsSpawnerTag>
    {
        
    }
}