using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
{
    public struct DamageZoneTag : IComponentData
    {
        
    }

    public struct DamageZonePrefab : IComponentData
    {
        public WeakObjectReference<DamageZoneView> Prefab;
    }
    
    public struct DamageZoneViewHolder : IComponentData
    {
        public UnityObjectRef<DamageZoneView> Value;
    }
    
    public struct DamageZoneRadius : IComponentData
    {
        public float Value;
    }
    
    public struct DamageZoneTargetRadius : IComponentData
    {
        public float Value;
    }
    
    public struct DamageZoneRadiusFactor : IComponentData
    {
        public float Value;
    }
    
        [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class DamageZoneViewSpawnerSystem : SystemBase
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<DamageZonePrefab>()
                .WithNone<DamageZoneViewHolder>()
                .Build();
            
            RequireForUpdate(_query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);
            foreach (var (prefabHolder, entity) in 
                     SystemAPI.Query<RefRO<DamageZonePrefab>>()
                         .WithNone<DamageZoneViewHolder>().WithEntityAccess())
            {
                var prefabRef = prefabHolder.ValueRO.Prefab;
                if (prefabRef.IsReferenceValid)
                {
                    // запускаем загрузку (если ещё не загружено)
                    if (prefabRef.LoadingStatus != ObjectLoadingStatus.Completed 
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Loading
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Queued)
                        prefabRef.LoadAsync();

                    if (prefabRef.LoadingStatus == ObjectLoadingStatus.Completed)
                    {
                        // var view = Object.Instantiate(prefabRef.Result);
                        var instance = ServiceLocator.Get<SpawnProvider>().Spawn(prefabRef.Result);
                        instance.SetPrefab(prefabHolder.ValueRO.Prefab);
                        ecb.AddComponent(entity, new DamageZoneViewHolder()
                        {
                            Value = instance, 
                        });
                        ecb.AddComponent(entity, new CleanupCallback()
                        {
                            Instance = instance.CleanupCallback, 
                        });
                    }
                }
            }
            
            ecb.Playback(EntityManager);
        }
    } 
    
    public partial struct DamageZoneUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            UpdatePosition(ref state);
            UpdateSize(ref state);
            UpdateView(ref state);
            UpdateDamage(ref state);
        }

        private void UpdateView(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);

            foreach (var (view, radius) in 
                     SystemAPI.Query<RefRO<DamageZoneViewHolder>, RefRO<DamageZoneRadius>>()
                         .WithAll<DamageZoneTag>())
            {
                view.ValueRO.Value.Value.SetPosition(playerTransform.Position);
                view.ValueRO.Value.Value.SetRadius(radius.ValueRO.Value);
            }
        }

        private void UpdatePosition(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);
            
            foreach (var transform in 
                     SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<DamageZoneTag>())
            {
                transform.ValueRW.Position = playerTransform.Position;
            }
        }

        private void UpdateSize(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, radius, targetRadius, radiusFactor) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<DamageZoneRadius>, RefRO<DamageZoneTargetRadius>, RefRO<DamageZoneRadiusFactor>>()
                         .WithAll<DamageZoneTag>())
            {
                var radiusValue = radius.ValueRO.Value;
                var targetRadiusValue = targetRadius.ValueRO.Value;
                // Debug.Log("Q");
                // Debug.Log(radiusValue);
                // Debug.Log(targetRadiusValue);
                if (math.abs(targetRadiusValue - radiusValue) > 0.001f)
                {
                    var sign = math.sign(targetRadiusValue - radiusValue);
                    var radiusDelta = sign * radiusFactor.ValueRO.Value * deltaTime;
                    // Debug.Log("IF");
                    // Debug.Log(sign);
                    // Debug.Log(radiusDelta);   
                    if (sign > 0)
                        radius.ValueRW.Value = math.clamp(radiusValue + radiusDelta, float.MinValue, targetRadiusValue);
                    else
                        radius.ValueRW.Value = math.clamp(radiusValue + radiusDelta, targetRadiusValue, float.MaxValue);
                    
                    transform.ValueRW.Scale = 2 * radius.ValueRW.Value;
                }
            }
        }
        
        private void UpdateDamage(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (zoneTransform, radius, damage) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<DamageZoneRadius>, RefRO<AttackDamage>>()
                         .WithAll<DamageZoneTag>())
            {
                var damageValue = damage.ValueRO.Value * deltaTime;
                foreach (var (enemyTransform, damageBuffer) in SystemAPI
                             .Query<RefRO<LocalTransform>, DynamicBuffer<DamageFrameBuffer>>()
                             .WithAll<EnemyTag>())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, enemyTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        damageBuffer.Add(new DamageFrameBuffer() { Value = damageValue });
                }
            }
        }
    }
}