using App.Ecs.Attack;
using App.Ecs.Health;
using App.Ecs.Moving;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs.Rockets
{
    public struct RocketTag : IComponentData
    {
        
    }
    
    public struct RocketAwaitTimer : IComponentData, IEnableableComponent
    {
        public float Value;
    }

    public struct RocketExplosionRadius : IComponentData
    {
        public float Value;
    }
    
    public struct RocketTargetHeight : IComponentData
    {
        public const float HeightError = 0.1f;
        public float Value;
    }
    
    public struct RocketViewExplosionRadiusSetFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    public partial struct RocketAwaitTimerTickSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RocketTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (rocketTimer, rocketTimerToggler) in 
                     SystemAPI.Query<RefRW<RocketAwaitTimer>, EnabledRefRW<RocketAwaitTimer>>()
                         .WithAll<RocketTag>())
            {
                rocketTimer.ValueRW.Value -= deltaTime;
                if (rocketTimer.ValueRO.Value > 0)
                    continue;

                rocketTimerToggler.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(IndependentMoveSystemGroup))]
    public partial struct RocketMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RocketTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, moveSpeed, targetHeight) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeed>, RefRO<RocketTargetHeight>>()
                         .WithAll<RocketTag>()
                         .WithDisabled<RocketAwaitTimer>())
            {
                transform.ValueRW.Position += -transform.ValueRW.Up() * moveSpeed.ValueRO.Value * deltaTime;
                if (transform.ValueRO.Position.y <= targetHeight.ValueRO.Value)
                {
                    var position = transform.ValueRO.Position;
                    position.y = targetHeight.ValueRO.Value;
                    transform.ValueRW.Position = position;
                }
            }
        }
    }
    
    [UpdateInGroup(typeof(PhysicsPausableSimulationGroup))]
    public partial struct RocketExplosionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, RocketTargetHeight, AttackDamage, RocketExplosionRadius, RocketTag>()
                .Build();
            
            state.RequireForUpdate(query);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physics = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (transform, targetHeight, damage, explosionRadius, rocketEntity) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<RocketTargetHeight>, RefRO<AttackDamage>, RefRO<RocketExplosionRadius>>()
                         .WithAll<RocketTag>()
                         .WithEntityAccess())
            {
                if (transform.ValueRO.Position.y <= targetHeight.ValueRO.Value + RocketTargetHeight.HeightError)
                {
                    var collisions = new NativeList<ColliderCastHit>(Allocator.Temp);
                    physics.SphereCastAll(transform.ValueRO.Position, explosionRadius.ValueRO.Value / 2, 
                        float3.zero, 0.1f, ref collisions,
                        new CollisionFilter()
                        {
                            BelongsTo = (uint)CollisionLayers.PlayerPerk, CollidesWith = (uint)CollisionLayers.Enemy
                        });

                    foreach (var collision in collisions)
                    {
                        if (SystemAPI.HasBuffer<DamageToHealthFrameBuffer>(collision.Entity))
                        {
                            var damageBuffer = SystemAPI.GetBuffer<DamageToHealthFrameBuffer>(collision.Entity);
                            damageBuffer.Add(new DamageToHealthFrameBuffer() { Value = damage.ValueRO.Value });
                        }
                    }
                    
                    ecb.DestroyEntity(rocketEntity);
                }
            }
        }
    }
}