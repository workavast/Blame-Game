using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace App.Ecs
{
    public struct PhysicsMassInitializeFlag : IComponentData, IEnableableComponent { }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PhysicsMassInitializeSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsMass, flag) in 
                     SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<PhysicsMassInitializeFlag>>())
            {
                physicsMass.ValueRW.InverseInertia = float3.zero;
                flag.ValueRW = false;
            }
        }
    }
}