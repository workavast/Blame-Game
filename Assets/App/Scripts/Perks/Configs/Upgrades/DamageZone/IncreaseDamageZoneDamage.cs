using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.DamageZone
{
    [CreateAssetMenu(fileName = nameof(IncreaseDamageZoneDamage), menuName = PerkConst.DamageZonePath + nameof(IncreaseDamageZoneDamage))]
    public class IncreaseDamageZoneDamage : IncreaseDamageScaleUpgrade<DamageZoneTag>
    {
    }
}