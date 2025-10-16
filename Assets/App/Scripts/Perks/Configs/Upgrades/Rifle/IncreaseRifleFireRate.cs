using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.Rifle;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Rifle
{
    [CreateAssetMenu(fileName = nameof(IncreaseRifleFireRate), menuName = PerkConst.RiflePath + nameof(IncreaseRifleFireRate))]
    public class IncreaseRifleFireRate : IncreaseFireRateUpgrade<RifleTag>
    {
        
    }
}