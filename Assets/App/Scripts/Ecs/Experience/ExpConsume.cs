using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Experience
{
    public struct ExpTag : IComponentData
    {
        
    }
    
    public struct PlayerExp : IComponentData
    {
        public float Value;
    }
    
    public struct ExpOrbConsumeMoveSpeed : IComponentData
    {
        public float MoveSpeed;
        public float Acceleration;
    }
    
    public struct ExpOrbPrefabHolder : IComponentData
    {
        public Entity OrbPrefab;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ExpOrbsConsumeMoveToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<ExpTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(playerEntity);
            
            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var expOrbConsumeData = SystemAPI.GetComponent<ExpOrbConsumeMoveSpeed>(expEntity);
            
            foreach (var (transform, moveDirection, moveSpeed) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRW<MoveSpeed>>()
                         .WithAll<ExpOrbTag, ExpOrbIsConsumeTag>())
            {
                moveDirection.ValueRW.Value = math.normalize(playerTransform.Position - transform.ValueRO.Position).xz;

                var moveSpeedValue = math.clamp(moveSpeed.ValueRO.Value + expOrbConsumeData.Acceleration * deltaTime, 0, expOrbConsumeData.MoveSpeed);
                moveSpeed.ValueRW.Value = moveSpeedValue;
            }
        }
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(ExpOrbsConsumeMoveToPlayerSystem))]
    public partial struct ExpOrbsConsumeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<ExpTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            var playerExpEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var playerExp = SystemAPI.GetComponentRW<PlayerExp>(playerExpEntity);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(playerEntity);
            
            foreach (var (transform, expAmount, entity) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<ExpOrbAmount>>()
                         .WithAll<ExpOrbTag, ExpOrbIsConsumeTag>()
                         .WithEntityAccess())
            {
                if (math.distance(playerTransform.Position.xz, transform.ValueRO.Position.xz) <= 0.1f)
                {
                    playerExp.ValueRW.Value += expAmount.ValueRO.Value;
                    ecb.DestroyEntity(entity); 
                }
            }
        }
    }
}