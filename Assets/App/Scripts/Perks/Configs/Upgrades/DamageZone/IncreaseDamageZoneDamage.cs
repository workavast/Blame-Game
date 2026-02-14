using App.Ecs.PlayerPerks.DamageZone;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.DamageZone
{
    [CreateAssetMenu(fileName = nameof(IncreaseDamageZoneDamage), menuName = PerksConsts.DamageZonePath + nameof(IncreaseDamageZoneDamage))]
    public class IncreaseDamageZoneDamage : IncreaseDamageScaleUpgrade<DamageZoneTag>
    {
    }
}