using Unity.Entities;

namespace App.Ecs.PlayerPerks
{
    public struct DefaultShootCooldown : IComponentData
    {
        public float Timer;
    }
    
    public struct ShootCooldown : IComponentData, IEnableableComponent
    {
        public float Timer;
    }
    
    public struct FireRateScale : IComponentData
    {
        public float Value;
    }
    
    public struct AdditionalProjectilesCount : IComponentData
    {
        public int Value;
    }
    
    public struct AdditionalPenetration : IComponentData
    {
        public int Value;
    }
    
    public struct ShootDistanceReaction : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    public partial struct ShootCooldownStarterSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (shootCooldown, defaultShootCooldown, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<ShootCooldown>, RefRO<DefaultShootCooldown>, EnabledRefRW<ShootCooldown>>()
                         .WithAll<IsActiveTag>())
            {
                if (shootCooldownToggler.ValueRO && shootCooldown.ValueRO.Timer <= 0)
                    shootCooldown.ValueRW.Timer = defaultShootCooldown.ValueRO.Timer;
            }
        }
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    [UpdateAfter(typeof(ShootCooldownStarterSystem))]
    public partial struct ShootCooldownTickSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            TickWithScale(ref state, deltaTime);
            TickWithoutScale(ref state, deltaTime);
        }

        private void TickWithScale(ref SystemState state, float deltaTime)
        {
            foreach (var (shootCooldown, scale, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<ShootCooldown>, RefRO<FireRateScale>, EnabledRefRW<ShootCooldown>>()
                         .WithAll<IsActiveTag>())
            {
                shootCooldown.ValueRW.Timer -= deltaTime * scale.ValueRO.Value;
                if (shootCooldown.ValueRO.Timer <= 0) 
                    shootCooldownToggler.ValueRW = false;
            }
        }
        
        private void TickWithoutScale(ref SystemState state, float deltaTime)
        {
            foreach (var (shootCooldown, shootCooldownToggler)  in
                     SystemAPI.Query<RefRW<ShootCooldown>, EnabledRefRW<ShootCooldown>>()
                         .WithAll<IsActiveTag>()
                         .WithNone<FireRateScale>())
            {
                shootCooldown.ValueRW.Timer -= deltaTime;
                if (shootCooldown.ValueRO.Timer <= 0) 
                    shootCooldownToggler.ValueRW = false;
            }
        }
    }
}