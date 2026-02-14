using App.Ecs.PlayerPerks.RocketLauncher;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.RocketLauncher
{
    [CreateAssetMenu(fileName = nameof(IncreaseRocketLauncherDamage), menuName = PerksConsts.RocketLauncherPath + nameof(IncreaseRocketLauncherDamage))]
    public class IncreaseRocketLauncherDamage : IncreaseDamageScaleUpgrade<RocketLauncherTag>
    {
        
    }
}