using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs
{
    public struct DefaultAttackCooldown : IComponentData
    {
        public float Timer;
    }
    
    public struct AttackCooldown : IComponentData, IEnableableComponent
    {
        public float Timer;
    }
    
    public struct AttackRateScale : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    public partial struct AttackCooldownStarterSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (shootCooldown, defaultShootCooldown, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<AttackCooldown>, RefRO<DefaultAttackCooldown>, EnabledRefRW<AttackCooldown>>())
            {
                if (shootCooldownToggler.ValueRO && shootCooldown.ValueRO.Timer <= 0)
                    shootCooldown.ValueRW.Timer = defaultShootCooldown.ValueRO.Timer;
            }
        }
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    [UpdateAfter(typeof(AttackCooldownStarterSystem))]
    public partial struct AttackCooldownTickSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            TickWithScale(ref state, deltaTime);
            TickWithoutScale(ref state, deltaTime);
        }

        private void TickWithScale(ref SystemState state, float deltaTime)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var globalFireRateScale = SystemAPI.GetComponent<AttackRateScale>(playerEntity);
            
            foreach (var (shootCooldown, scale, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<AttackCooldown>, RefRO<AttackRateScale>, EnabledRefRW<AttackCooldown>>())
            {
                shootCooldown.ValueRW.Timer -= deltaTime * (scale.ValueRO.Value + globalFireRateScale.Value);
                if (shootCooldown.ValueRO.Timer <= 0) 
                    shootCooldownToggler.ValueRW = false;
            }
        }
        
        private void TickWithoutScale(ref SystemState state, float deltaTime)
        {
            foreach (var (shootCooldown, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<AttackCooldown>, EnabledRefRW<AttackCooldown>>()
                         .WithNone<AttackRateScale>())
            {
                shootCooldown.ValueRW.Timer -= deltaTime;
                if (shootCooldown.ValueRO.Timer <= 0) 
                    shootCooldownToggler.ValueRW = false;
            }
        }
    }
}