using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Rifle
{
    [CreateAssetMenu(fileName = nameof(IncreaseRifleDamage), menuName = PerkConst.RiflePath + nameof(IncreaseRifleDamage))]
    public class IncreaseRifleDamage : IncreaseDamageScaleUpgrade<RifleTag>
    {
        
    }
}