using App.Ecs.PlayerPerks.RocketLauncher;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.RocketLauncher
{
    [CreateAssetMenu(fileName = nameof(IncreaseRocketLauncherFireRate), menuName = PerksConsts.RocketLauncherPath + nameof(IncreaseRocketLauncherFireRate))]
    public class IncreaseRocketLauncherFireRate : IncreaseFireRateUpgrade<RocketLauncherTag>
    {
        
    }
}