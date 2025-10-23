using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.MachineGun
{
    [CreateAssetMenu(fileName = nameof(IncreaseMachineGunPenetration), menuName = PerkConst.MachineGunPath + nameof(IncreaseMachineGunPenetration))]
    public class IncreaseMachineGunPenetration : IncreasePenetrationUpgrade<MachineGunTag>
    {
        
    }
}