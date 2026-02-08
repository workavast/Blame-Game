using App.Ecs.PlayerPerks.MachineGun;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.MachineGun
{
    [CreateAssetMenu(fileName = nameof(IncreaseMachineGunPenetration), menuName = PerksConsts.MachineGunPath + nameof(IncreaseMachineGunPenetration))]
    public class IncreaseMachineGunPenetration : IncreasePenetrationUpgrade<MachineGunTag>
    {
        
    }
}