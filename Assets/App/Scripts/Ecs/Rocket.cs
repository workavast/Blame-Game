using App.Views;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
{
    public struct RocketTag : IComponentData
    {
        
    }
    
    public struct RocketViewHolder : IComponentData
    {
        public UnityObjectRef<RocketView> Instance;
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
    
    public partial class RocketViewInitializeSystem : ViewInstallerSystem<RocketTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new RocketViewHolder() { Instance = instance as RocketView });
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(ViewInstallSystemGroup))]
    public partial struct RocketViewExplosionRadiusInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (radiusSeted, viewHolder, explosionRadius)  in 
                     SystemAPI.Query<EnabledRefRW<RocketViewExplosionRadiusSetFlag>, RefRO<RocketViewHolder>, RefRO<RocketExplosionRadius>>()
                         .WithAll<RocketTag>())
            {
                viewHolder.ValueRO.Instance.Value.SetExplosionRadius(explosionRadius.ValueRO.Value);
                radiusSeted.ValueRW = false;
            }
        }
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct RocketAwaitTimerTickSystem : ISystem
    {
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
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct RocketMoveSystem : ISystem
    {
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
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(RocketMoveSystem))]
    public partial struct RocketViewUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, viewHolder) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<RocketViewHolder>>()
                         .WithAll<RocketTag>())
            {
                viewHolder.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(RocketMoveSystem))]
    public partial struct RocketExplosionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var physics = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (transform, targetHeight, damage, explosionRadius, rocketEntity) in 
                     SystemAPI.Query< RefRO<LocalToWorld>, RefRO<RocketTargetHeight>, RefRO<AttackDamage>, RefRO<RocketExplosionRadius>>()
                         .WithAll<RocketTag>()
                         .WithEntityAccess())
            {
                var collisions = new NativeList<ColliderCastHit>(Allocator.Temp);
                if (transform.ValueRO.Position.y <= targetHeight.ValueRO.Value + RocketTargetHeight.HeightError)
                {
                    physics.SphereCastAll(transform.ValueRO.Position, explosionRadius.ValueRO.Value / 2, 
                        float3.zero, 0.1f, ref collisions,
                        new CollisionFilter()
                        {
                            BelongsTo = (uint)CollisionLayers.PlayerPerk, CollidesWith = (uint)CollisionLayers.Enemy
                        });

                    foreach (var collision in collisions)
                    {
                        if (SystemAPI.HasBuffer<DamageFrameBuffer>(collision.Entity))
                        {
                            var damageBuffer = SystemAPI.GetBuffer<DamageFrameBuffer>(collision.Entity);
                            damageBuffer.Add(new DamageFrameBuffer() { Value = damage.ValueRO.Value });
                        }
                    }
                    
                    ecb.DestroyEntity(rocketEntity);
                }
            }
        }
    }
}