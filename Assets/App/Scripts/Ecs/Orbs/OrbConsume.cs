using App.Ecs.Moving;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs.Orbs
{
    public struct OrbGlobalDataTag : IComponentData
    {
        
    }

    public struct OrbConsumeDistanceError : IComponentData
    {
        public float Value;
    }
    
    public struct OrbConsumeMoveSpeed : IComponentData
    {
        public float MoveSpeed;
        public float Acceleration;
    }

    public struct OrbConsumeTag : IComponentData
    {
        
    }
    
    public struct OrbConsumedTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(DefaultMoveSystem))]
    public partial struct OrbsConsumeMoveToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<OrbGlobalDataTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            
            var orbEntity = SystemAPI.GetSingletonEntity<OrbGlobalDataTag>();
            var orbConsumeData = SystemAPI.GetComponent<OrbConsumeMoveSpeed>(orbEntity);
            
            foreach (var (transform, moveDirection, moveSpeed) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRW<MoveSpeed>>()
                         .WithAll<OrbTag, OrbConsumeTag>()
                         .WithNone<OrbConsumedTag>())
            {
                moveDirection.ValueRW.Value = math.normalize(playerTransform.Position.xz - transform.ValueRO.Position.xz);

                var moveSpeedValue = math.clamp(moveSpeed.ValueRO.Value + orbConsumeData.Acceleration * deltaTime, 0, orbConsumeData.MoveSpeed);
                moveSpeed.ValueRW.Value = moveSpeedValue;
            }
        }
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct OrbsCheckConsumeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<OrbGlobalDataTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var orbGlobalDataEntity = SystemAPI.GetSingletonEntity<OrbGlobalDataTag>();
            var orbConsumeDistanceError = SystemAPI.GetComponent<OrbConsumeDistanceError>(orbGlobalDataEntity);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);

            foreach (var (transform, entity) in
                     SystemAPI.Query<RefRO<LocalTransform>>()
                         .WithAll<OrbTag, OrbConsumeTag>()
                         .WithNone<OrbConsumedTag>()
                         .WithEntityAccess())
            {
                var dist = math.distance(playerTransform.Position.xz, transform.ValueRO.Position.xz);
                if (dist <= orbConsumeDistanceError.Value) 
                    ecb.AddComponent(entity, new OrbConsumedTag());
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct OrbsDestroySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (orbTag, entity) in 
                     SystemAPI.Query<RefRO<OrbTag>>()
                         .WithAll<OrbConsumedTag>()
                         .WithEntityAccess())
            {
                Debug.Log("DESTROY");
                ecb.DestroyEntity(entity);
            }
        }
    }
}