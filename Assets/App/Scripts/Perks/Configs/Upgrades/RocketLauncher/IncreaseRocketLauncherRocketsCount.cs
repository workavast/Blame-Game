using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.RocektLauncher;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.RocketLauncher
{
    [CreateAssetMenu(fileName = nameof(IncreaseRocketLauncherRocketsCount), menuName = PerkConst.RocketLauncherPath + nameof(IncreaseRocketLauncherRocketsCount))]
    public class IncreaseRocketLauncherRocketsCount : IncreaseProjectilesCountUpgrade<RocketLauncherTag>
    {
        
    }
}