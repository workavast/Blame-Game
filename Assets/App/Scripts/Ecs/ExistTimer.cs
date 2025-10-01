using Unity.Entities;

namespace App.Ecs
{
    public struct ExistTimer : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct ExistTimerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var existTimer in 
                     SystemAPI.Query<RefRW<ExistTimer>>()
                         .WithAll<IsActiveTag>())
            {
                existTimer.ValueRW.Value -= deltaTime;
            }
        }
    }
}