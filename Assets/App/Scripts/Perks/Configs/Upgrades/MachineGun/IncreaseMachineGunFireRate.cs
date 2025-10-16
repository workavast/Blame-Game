using App.Ecs.PlayerPerks;
using App.Ecs.PlayerPerks.MachineGun;
using UnityEngine;

namespace App.Perks.Configs.Upgrades.MachineGun
{
    [CreateAssetMenu(fileName = nameof(IncreaseMachineGunFireRate), menuName = PerkConst.MachineGunPath + nameof(IncreaseMachineGunFireRate))]
    public class IncreaseMachineGunFireRate : IncreaseFireRateUpgrade<MachineGunTag>
    {
        
    }
}