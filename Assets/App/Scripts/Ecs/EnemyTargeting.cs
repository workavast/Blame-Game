using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs
{
    public struct EnemyTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateBefore(typeof(LookAtPointSystem))]
    public partial struct EnemiesTargetingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);

            foreach (var (transform, moveDirection, lookPoint) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRW<LookPoint>>()
                         .WithAll<EnemyTag>())
            {
                var moveDirectionV3 = playerTransform.Position - transform.ValueRO.Position;
                moveDirection.ValueRW.Value = math.normalize(moveDirectionV3.xz);
                lookPoint.ValueRW.Value = playerTransform.Position;
            }
        }
    }
}