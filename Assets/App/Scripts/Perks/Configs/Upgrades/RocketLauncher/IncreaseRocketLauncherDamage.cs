using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.RocektLauncher;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.RocketLauncher
{
    [CreateAssetMenu(fileName = nameof(IncreaseRocketLauncherDamage), menuName = PerkConst.RocketLauncherPath + nameof(IncreaseRocketLauncherDamage))]
    public class IncreaseRocketLauncherDamage : IncreaseDamageScaleUpgrade<RocketLauncherTag>
    {
        
    }
}