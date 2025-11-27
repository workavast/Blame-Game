using App.Ecs.Looking;
using App.Ecs.Moving;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
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
                         .WithAll<EnemyTag>())
            {
                lookPoint.ValueRW.Value = playerTransform.Position;
            }
        }
    }
    
    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(DefaultMoveSystem))]
    public partial struct EnemiesDefaultMoveDirectionToPlayerSystem : ISystem
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
                         .WithAll<EnemyTag, DefaultMoveTag>())
            {
                var moveDirectionV3 = playerTransform.Position - transform.ValueRO.Position;
                moveDirection.ValueRW.Value = math.normalizesafe(moveDirectionV3.xz);
            }
        }
    }
}