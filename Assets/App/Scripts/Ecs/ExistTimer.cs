using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs
{
    public struct ExistTimer : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    public partial struct ExistTimerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var existTimer in 
                     SystemAPI.Query<RefRW<ExistTimer>>())
            {
                existTimer.ValueRW.Value -= deltaTime;
            }
        }
    }
}