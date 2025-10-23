using App.Ecs.PlayerPerks;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.MachineGun
{
    [CreateAssetMenu(fileName = nameof(IncreaseMachineGunDamage), menuName = PerkConst.MachineGunPath + nameof(IncreaseMachineGunDamage))]
    public class IncreaseMachineGunDamage : IncreaseDamageScaleUpgrade<MachineGunTag>
    {
        
    }
}