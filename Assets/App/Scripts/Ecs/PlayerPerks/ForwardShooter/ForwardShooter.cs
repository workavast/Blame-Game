using App.Ecs.Bullets;
using App.Ecs.Clenuping;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs.PlayerPerks.ForwardShooter
{
    public struct ForwardShooterTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ForwardShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            var globalDamageScale = SystemAPI.GetComponent<DamageScale>(playerEntity);
            
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, damageScale, 
                         additionalPenetration, sfxView, entity) in 
                     SystemAPI.Query<RefRO<BulletInitialData>, RefRO<DamageScale>,
                             RefRO<AdditionalPenetration>, RefRO<ShooterSfxViewHolder>>()
                         .WithAll<ForwardShooterTag>()
                         .WithDisabled<AttackCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);
                
                var bullet = ecb.Instantiate(data.ValueRO.BulletPrefab);
                var bulletSpawnPosition = playerTransform.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(bulletSpawnPosition, playerTransform.Rotation));
                
                
                BulletBuilder.Build(ref ecb, ref bullet, data, damageScale, globalDamageScale, additionalPenetration);

                sfxView.ValueRO.Instance.Value.PlaySfx(playerTransform.Position);
            }
        }
    }
}