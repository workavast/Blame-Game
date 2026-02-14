using App.Ecs.Player;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.Global
{
    [CreateAssetMenu(fileName = nameof(IncreaseGlobalFireRate), menuName = PerksConsts.GlobalPath + nameof(IncreaseGlobalFireRate))]
    public class IncreaseGlobalFireRate : IncreaseFireRateUpgrade<PlayerTag>
    {
        
    }
}