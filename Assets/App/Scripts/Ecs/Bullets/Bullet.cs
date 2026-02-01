using App.Ecs.Attack;
using App.Ecs.EntityViews;
using App.Ecs.Health;
using App.Ecs.Moving;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs.Bullets
{
    public struct BulletTag : IComponentData
    {
        
    }

    public struct BulletPenetration : IComponentData
    {
        public int Value;
    }

    public struct BulletCollisions : IBufferElementData
    {
        public Entity Entity;
    }
    
    public struct BulletInitialData : IComponentData
    {
        public Entity BulletPrefab;
        public float SpawnVerticalOffset;
        public float Damage;
        public float MoveSpeed;
    }

    [UpdateInGroup(typeof(IndependentMoveSystemGroup))]
    public partial struct BulletMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, moveSpeed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeed>>()
                         .WithAll<BulletTag>())
            {
                transform.ValueRW.Position += transform.ValueRW.Forward() * moveSpeed.ValueRO.Value * deltaTime;
            }
        }
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    [UpdateAfter(typeof(ExistTimerSystem))]
    public partial struct BulletExistTimeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<BulletTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (existTimer, entity) in 
                     SystemAPI.Query<RefRO<ExistTimer>>()
                         .WithAll<BulletTag>()
                         .WithEntityAccess())
            {
                if (existTimer.ValueRO.Value <= 0) 
                    ecb.DestroyEntity(entity);
            }
        }
    }
    
    [UpdateInGroup(typeof(PhysicsPausableSimulationGroup))]
    public partial struct BulletCollisionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<BulletTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var bulletCollisionJob = new BulletCollisionJob()
            {
                DamageableLookup = SystemAPI.GetComponentLookup<CurrentHealth>(true),
                BulletLookup = SystemAPI.GetComponentLookup<BulletTag>(true),
                AttackDamageLookup = SystemAPI.GetComponentLookup<AttackDamage>(true),
                BulletPenetrationLookup = SystemAPI.GetComponentLookup<BulletPenetration>(true),
                    
                Ecb = ecb.AsParallelWriter(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageToHealthFrameBuffer>(),
                BulletCollisionsLookup = SystemAPI.GetBufferLookup<BulletCollisions>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = bulletCollisionJob.Schedule(simulationSingleton, state.Dependency);
        }

        private struct BulletCollisionJob : ITriggerEventsJob
        {
            [ReadOnly] public ComponentLookup<CurrentHealth> DamageableLookup;
            [ReadOnly] public ComponentLookup<BulletTag> BulletLookup;
            [ReadOnly] public ComponentLookup<AttackDamage> AttackDamageLookup;
            [ReadOnly] public ComponentLookup<BulletPenetration> BulletPenetrationLookup;

            public EntityCommandBuffer.ParallelWriter Ecb;
            public BufferLookup<DamageToHealthFrameBuffer> DamageBufferLookup;
            public BufferLookup<BulletCollisions> BulletCollisionsLookup;
        
            public void Execute(TriggerEvent triggerEvent)
            {
                Entity target;
                Entity bullet;

                if (DamageableLookup.HasComponent(triggerEvent.EntityA) && BulletLookup.HasComponent(triggerEvent.EntityB))
                {
                    target = triggerEvent.EntityA;
                    bullet = triggerEvent.EntityB;
                } 
                else if (DamageableLookup.HasComponent(triggerEvent.EntityB) && BulletLookup.HasComponent(triggerEvent.EntityA))
                {
                    target = triggerEvent.EntityB;
                    bullet = triggerEvent.EntityA;
                }
                else
                {
                    return;
                }

                var collisions = BulletCollisionsLookup[bullet];
                for (var i = 0; i < collisions.Length; i++)
                    if (collisions[i].Entity == target)
                        return;
            
                var attack = AttackDamageLookup.GetRefRO(bullet);
                var penetration = BulletPenetrationLookup.GetRefRO(bullet);

                var enemyDamageBuffer = DamageBufferLookup[target];
                enemyDamageBuffer.Add(new DamageToHealthFrameBuffer() {Value = attack.ValueRO.Value});

                //if collisions.Length + 1 more than penetration, we may not add collision, we can just destroy bullet 
                if (collisions.Length + 1 > penetration.ValueRO.Value) 
                    Ecb.DestroyEntity(0, bullet);
                else
                    collisions.Add(new BulletCollisions() { Entity = target });
            }
        }
    }
}