using App.Ecs.Experience.ExpConsumeZone;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.ExpZone
{
    [CreateAssetMenu(fileName = nameof(IncreaseExpZoneRadius), menuName = PerksConsts.ExpZonePath + nameof(IncreaseExpZoneRadius))]
    public class IncreaseExpZoneRadius : IncreaseAoeZoneRadius<ExpConsumeZoneTag>
    {

    }
}