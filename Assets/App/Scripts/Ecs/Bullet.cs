using App.Ecs.PlayerPerks;
using App.Views;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
{
    
    #region components
    
    public struct BulletTag : IComponentData
    {
        
    }

    public struct BulletViewHolder : IComponentData
    {
        public UnityObjectRef<BulletView> Instance;
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
        public int Penetration;
    }
    
    #endregion

    public struct BulletBuilder
    {
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration });
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<DamageScale> damageScale)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration });
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<DamageScale> damageScale, RefRO<AdditionalPenetration> additionalPenetration)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration + additionalPenetration.ValueRO.Value});
        }
    }

    public partial class BulletViewInstallerSystem : ViewInstallerSystem<BulletTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new BulletViewHolder() { Instance = instance as BulletView });
    }
    
    [UpdateInGroup(typeof(IndependentMoveSystemGroup))]
    public partial struct BulletMoveSystem : ISystem
    {
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
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct BulletViewUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (view, transform) in 
                     SystemAPI.Query<RefRO<BulletViewHolder>, RefRO<LocalTransform>>()
                         .WithAll<IsActiveTag, BulletTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
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
        }

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
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var bulletCollisionJob = new BulletCollisionJob()
            {
                DamageableLookup = SystemAPI.GetComponentLookup<CurrentHealth>(true),
                BulletLookup = SystemAPI.GetComponentLookup<BulletTag>(true),
                AttackDamageLookup = SystemAPI.GetComponentLookup<AttackDamage>(true),
                BulletPenetrationLookup = SystemAPI.GetComponentLookup<BulletPenetration>(),
                    
                ECB = ecb.AsParallelWriter(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageFrameBuffer>(),
                BulletCollisionsLookup = SystemAPI.GetBufferLookup<BulletCollisions>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = bulletCollisionJob.Schedule(simulationSingleton, state.Dependency);
        }
    }

    public struct BulletCollisionJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<CurrentHealth> DamageableLookup;
        [ReadOnly] public ComponentLookup<BulletTag> BulletLookup;
        [ReadOnly] public ComponentLookup<AttackDamage> AttackDamageLookup;
        [ReadOnly] public ComponentLookup<BulletPenetration> BulletPenetrationLookup;

        public EntityCommandBuffer.ParallelWriter ECB;
        public BufferLookup<DamageFrameBuffer> DamageBufferLookup;
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

            collisions.Add(new BulletCollisions() { Entity = target });
            enemyDamageBuffer.Add(new DamageFrameBuffer() {Value = attack.ValueRO.Value});

            if (collisions.Length > penetration.ValueRO.Value) 
                ECB.DestroyEntity(0, bullet);
        }
    }
}