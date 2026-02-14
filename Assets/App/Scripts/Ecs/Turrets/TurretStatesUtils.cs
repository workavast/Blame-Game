using App.Ecs.Shooting.Ammo;
using App.Ecs.Turrets.Deployment;
using App.Ecs.Turrets.ReadyToUse;
using Unity.Entities;

namespace App.Ecs.Turrets
{
    public struct TurretStatesUtils
    {
        public static void SetDeploymentState(Entity entity, ref EntityCommandBuffer ecb)
        {
            ecb.AddComponent<TurretStateDeploymentTag>(entity);
        }
        
        public static void SetReadyToUseState(Entity entity, ref EntityCommandBuffer ecb)
        {
            ecb.AddComponent<TurretStateReadyToUseTag>(entity);
            ecb.AddComponent<AmmoCapacityViewIsVisibleTag>(entity);
        }
    }
}