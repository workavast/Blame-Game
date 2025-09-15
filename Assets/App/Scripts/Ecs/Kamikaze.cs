using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace App.Ecs
{
    public struct KamikazeTag : IComponentData
    {
        
    }

    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial struct KamikazeExplosionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var kamikazeExplosionJob = new KamikazeExplosionJob()
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(true),
                KamikazeLookup = SystemAPI.GetComponentLookup<KamikazeTag>(true),
                AttackDamageLookup = SystemAPI.GetComponentLookup<AttackDamage>(true),
                    
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageFrameBuffer>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = kamikazeExplosionJob.Schedule(simulationSingleton, state.Dependency);
        }
    }

    public struct KamikazeExplosionJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<KamikazeTag> KamikazeLookup;
        [ReadOnly] public ComponentLookup<AttackDamage> AttackDamageLookup;
        
        public BufferLookup<DamageFrameBuffer> DamageBufferLookup;
        
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity player;
            Entity kamikaze;

            if (PlayerLookup.HasComponent(collisionEvent.EntityA) && KamikazeLookup.HasComponent(collisionEvent.EntityB))
            {
                player = collisionEvent.EntityA;
                kamikaze = collisionEvent.EntityB;
            } 
            else if (PlayerLookup.HasComponent(collisionEvent.EntityB) && KamikazeLookup.HasComponent(collisionEvent.EntityA))
            {
                player = collisionEvent.EntityB;
                kamikaze = collisionEvent.EntityA;
            }
            else
            {
                return;
            }

            var attack = AttackDamageLookup.GetRefRO(kamikaze);
            var playerDamageBuffer = DamageBufferLookup[player];
            var kamikazeDamageBuffer = DamageBufferLookup[kamikaze];

            playerDamageBuffer.Add(new DamageFrameBuffer() {Value = attack.ValueRO.Value});
            kamikazeDamageBuffer.Add(new DamageFrameBuffer() {Value = float.MaxValue});
        }
    }
}