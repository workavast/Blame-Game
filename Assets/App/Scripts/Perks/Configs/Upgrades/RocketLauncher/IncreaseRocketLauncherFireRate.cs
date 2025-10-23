using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.RocketLauncher
{
    [CreateAssetMenu(fileName = nameof(IncreaseRocketLauncherFireRate), menuName = PerkConst.RocketLauncherPath + nameof(IncreaseRocketLauncherFireRate))]
    public class IncreaseRocketLauncherFireRate : IncreaseFireRateUpgrade<RocketLauncherTag>
    {
        
    }
}