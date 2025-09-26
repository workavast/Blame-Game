using App.Views;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace App.Ecs
{
    public struct BulletTag : IComponentData
    {
        
    }

    public struct BulletViewHolder : IComponentData
    {
        public UnityObjectRef<BulletView> Instance;
    }
    
    public partial class BulletViewInstallerSystem : ViewInstallerSystem<BulletTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new BulletViewHolder() { Instance = instance as BulletView });
    }
    
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
    
    public partial struct BulletViewUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (view, transform) in 
                     SystemAPI.Query<RefRO<BulletViewHolder>, RefRO<LocalTransform>>()
                         .WithAll<BulletTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
    
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
    
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
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
                EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(true),
                BulletLookup = SystemAPI.GetComponentLookup<BulletTag>(true),
                AttackDamageLookup = SystemAPI.GetComponentLookup<AttackDamage>(true),
                    
                ECB = ecb.AsParallelWriter(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageFrameBuffer>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = bulletCollisionJob.Schedule(simulationSingleton, state.Dependency);
        }
    }

    public struct BulletCollisionJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<EnemyTag> EnemyLookup;
        [ReadOnly] public ComponentLookup<BulletTag> BulletLookup;
        [ReadOnly] public ComponentLookup<AttackDamage> AttackDamageLookup;

        public EntityCommandBuffer.ParallelWriter ECB;
        public BufferLookup<DamageFrameBuffer> DamageBufferLookup;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity enemy;
            Entity bullet;

            if (EnemyLookup.HasComponent(triggerEvent.EntityA) && BulletLookup.HasComponent(triggerEvent.EntityB))
            {
                enemy = triggerEvent.EntityA;
                bullet = triggerEvent.EntityB;
            } 
            else if (EnemyLookup.HasComponent(triggerEvent.EntityB) && BulletLookup.HasComponent(triggerEvent.EntityA))
            {
                enemy = triggerEvent.EntityB;
                bullet = triggerEvent.EntityA;
            }
            else
            {
                return;
            }

            var attack = AttackDamageLookup.GetRefRO(bullet);
            var enemyDamageBuffer = DamageBufferLookup[enemy];

            enemyDamageBuffer.Add(new DamageFrameBuffer() {Value = attack.ValueRO.Value});
            ECB.DestroyEntity(0, bullet);
        }
    }
}