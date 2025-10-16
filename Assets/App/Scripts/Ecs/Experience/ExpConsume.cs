using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs.Experience
{
    public struct ExpTag : IComponentData
    {
        
    }

    public struct ExpScale : IComponentData
    {
        public float Value;
    }
    
    public struct PlayerExp : IComponentData
    {
        public float Value;
    }
    
    public struct ExpOrbConsumeDistanceError : IComponentData
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
    
    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(AutoMoveSystem))]
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
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            
            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var expOrbConsumeData = SystemAPI.GetComponent<ExpOrbConsumeMoveSpeed>(expEntity);
            
            foreach (var (transform, moveDirection, moveSpeed) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRW<MoveSpeed>>()
                         .WithAll<ExpOrbTag, ExpOrbIsConsumeTag>())
            {
                moveDirection.ValueRW.Value = math.normalize(playerTransform.Position.xz - transform.ValueRO.Position.xz);

                var moveSpeedValue = math.clamp(moveSpeed.ValueRO.Value + expOrbConsumeData.Acceleration * deltaTime, 0, expOrbConsumeData.MoveSpeed);
                moveSpeed.ValueRW.Value = moveSpeedValue;
            }
        }
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
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

            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var playerExp = SystemAPI.GetComponentRW<PlayerExp>(expEntity);
            var expOrbConsumeDistanceError = SystemAPI.GetComponent<ExpOrbConsumeDistanceError>(expEntity);
            
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            var globalExpScale = SystemAPI.GetComponent<ExpScale>(playerEntity);

            foreach (var (transform, expAmount, entity) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<ExpOrbAmount>>()
                         .WithAll<ExpOrbTag, ExpOrbIsConsumeTag>()
                         .WithEntityAccess())
            {
                var dist = math.distance(playerTransform.Position.xz, transform.ValueRO.Position.xz);
                if (dist <= expOrbConsumeDistanceError.Value)
                {
                    playerExp.ValueRW.Value += expAmount.ValueRO.Value * globalExpScale.Value;
                    ecb.DestroyEntity(entity); 
                }
            }
        }
    }
}