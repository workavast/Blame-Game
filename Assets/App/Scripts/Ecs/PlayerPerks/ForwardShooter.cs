using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks
{
    public struct ForwardShooterTag : IComponentData
    {
        
    }

    public struct ForwardShooterData : IComponentData
    {
        public Entity BulletPrefab;
        public float SpawnVerticalOffset;
        public float Damage;
        public float MoveSpeed;
        public float ShootPause;
    }
    
    public struct ForwardShooterPause : IComponentData
    {
        public float Timer;
    }
    
    public partial struct ForwardShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);

            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, pause) in 
                     SystemAPI.Query<RefRO<ForwardShooterData>, RefRW<ForwardShooterPause>>()
                         .WithAll<ForwardShooterTag>())
            {
                pause.ValueRW.Timer -= deltaTime;
                if (pause.ValueRO.Timer > 0)
                     continue;

                pause.ValueRW.Timer = data.ValueRO.ShootPause;

                var bullet = ecb.Instantiate(data.ValueRO.BulletPrefab);
                var bulletSpawnPosition = playerTransform.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(bulletSpawnPosition, playerTransform.Rotation));
                ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage });
                ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            }
        }
    }
}