using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.DamageZone;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.DamageZone
{
    [CreateAssetMenu(fileName = nameof(IncreaseDamageZoneDamage), menuName = PerkConst.DamageZonePath + nameof(IncreaseDamageZoneDamage))]
    public class IncreaseDamageZoneDamage : IncreaseDamageScaleUpgrade<DamageZoneTag>
    {
    }
}