using App.Ecs;
using App.Ecs.Player;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseGlobalFireRate), menuName = PerkConst.GlobalPath + nameof(IncreaseGlobalFireRate))]
    public class IncreaseGlobalFireRate : IncreaseFireRateUpgrade<PlayerTag>
    {
        
    }
}