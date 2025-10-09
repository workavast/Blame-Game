using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Enemies
{
    public struct EnemyTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    [UpdateAfter(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(LookAtPointSystem))]
    public partial struct EnemiesLookAtPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);

            foreach (var lookPoint in SystemAPI.Query<RefRW<LookPoint>>()
                         .WithAll<IsActiveTag, EnemyTag>())
            {
                lookPoint.ValueRW.Value = playerTransform.Position;
            }
        }
    }
    
    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(AutoMoveSystem))]
    public partial struct EnemiesAutoMoveToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(player);

            foreach (var (transform, moveDirection) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>>()
                         .WithAll<IsActiveTag, EnemyTag, AutoMoveTag>())
            {
                var moveDirectionV3 = playerTransform.Position - transform.ValueRO.Position;
                moveDirection.ValueRW.Value = math.normalizesafe(moveDirectionV3.xz);
            }
        }
    }
}