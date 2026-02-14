using App.Ecs.PlayerPerks.MachineGun;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.MachineGun
{
    [CreateAssetMenu(fileName = nameof(IncreaseMachineGunDamage), menuName = PerksConsts.MachineGunPath + nameof(IncreaseMachineGunDamage))]
    public class IncreaseMachineGunDamage : IncreaseDamageScaleUpgrade<MachineGunTag>
    {
        
    }
}