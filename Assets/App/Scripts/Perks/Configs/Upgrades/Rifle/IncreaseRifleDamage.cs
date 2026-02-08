using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.Rifle;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Rifle
{
    [CreateAssetMenu(fileName = nameof(IncreaseRifleDamage), menuName = PerksConsts.RiflePath + nameof(IncreaseRifleDamage))]
    public class IncreaseRifleDamage : IncreaseDamageScaleUpgrade<RifleTag>
    {
        
    }
}