using App.Ecs.PlayerPerks.DamageZone;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.DamageZone
{
    [CreateAssetMenu(fileName = nameof(IncreaseDamageZoneRadius), menuName = PerksConsts.DamageZonePath + nameof(IncreaseDamageZoneRadius))]
    public class IncreaseDamageZoneRadius : IncreaseAoeZoneRadius<DamageZoneTag>
    {

    }
}