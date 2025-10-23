using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace App.Ecs.Enemies.MeleeBot
{
    public struct MeleeBotTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(PhysicsPausableSimulationGroup))]
    public partial struct MeleeAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var meleeAttackJob = new MeleeAttackJob()
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(true),
                MeleeBotLookup = SystemAPI.GetComponentLookup<MeleeBotTag>(true),
                AttackDamageLookup = SystemAPI.GetComponentLookup<AttackDamage>(true),
                    
                ShootCooldownLookup = SystemAPI.GetComponentLookup<AttackCooldown>(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageFrameBuffer>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = meleeAttackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
    
    public struct MeleeAttackJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<MeleeBotTag> MeleeBotLookup;
        [ReadOnly] public ComponentLookup<AttackDamage> AttackDamageLookup;
        
        public ComponentLookup<AttackCooldown> ShootCooldownLookup;
        public BufferLookup<DamageFrameBuffer> DamageBufferLookup;
        
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity player;
            Entity meleeBot;

            if (PlayerLookup.HasComponent(collisionEvent.EntityA) && MeleeBotLookup.HasComponent(collisionEvent.EntityB))
            {
                player = collisionEvent.EntityA;
                meleeBot = collisionEvent.EntityB;
            } 
            else if (PlayerLookup.HasComponent(collisionEvent.EntityB) && MeleeBotLookup.HasComponent(collisionEvent.EntityA))
            {
                player = collisionEvent.EntityB;
                meleeBot = collisionEvent.EntityA;
            }
            else
            {
                return;
            }

            var cooldown = ShootCooldownLookup.GetEnabledRefRW<AttackCooldown>(meleeBot);
            if (cooldown.ValueRO)//in cooldown process
                return;

            cooldown.ValueRW = true;
            var attack = AttackDamageLookup.GetRefRO(meleeBot);
            var playerDamageBuffer = DamageBufferLookup[player];

            playerDamageBuffer.Add(new DamageFrameBuffer() {Value = attack.ValueRO.Value});
        }
    }
}