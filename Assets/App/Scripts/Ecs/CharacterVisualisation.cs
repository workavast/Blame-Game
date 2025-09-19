using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
{
    public struct CharacterVisualPrefab : IComponentData
    {
        public WeakObjectReference<CharacterView> Prefab;
    }
    
    public struct CharacterVisual : IComponentData
    {
        public UnityObjectRef<CharacterView> Instance;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class CharacterVisualisationSpawnerSystem : SystemBase
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<CharacterVisualPrefab>()
                .WithNone<CharacterVisual>()
                .Build();
            
            RequireForUpdate(_query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);
            foreach (var (viewPrefabHolder, entity) in 
                     SystemAPI.Query<RefRO<CharacterVisualPrefab>>()
                         .WithNone<CharacterVisual>().WithEntityAccess())
            {
                var prefabRef = viewPrefabHolder.ValueRO.Prefab;
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
                        instance.SetPrefab(viewPrefabHolder.ValueRO.Prefab);
                        ecb.AddComponent(entity, new CharacterVisual
                        {
                            Instance = instance, 
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
    
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct PhysicsCharacterVisualisationUpdateSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, PhysicsVelocity, CharacterVisual>()
                .Build();

            state.RequireForUpdate(_query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, physicsVelocity, characterVisual) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<PhysicsVelocity>, RefRW<CharacterVisual>>())
            {
                characterVisual.ValueRO.Instance.Value.SetVelocity(physicsVelocity.ValueRO.Linear);
                characterVisual.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                characterVisual.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
    
    public partial struct CharacterVisualisationUpdateSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, CharacterVisual>()
                .WithNone<PhysicsVelocity>()
                .Build();

            state.RequireForUpdate(_query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, characterVisual) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CharacterVisual>>()
                         .WithNone<PhysicsVelocity>())
            {
                characterVisual.ValueRO.Instance.Value.SetVelocity(float3.zero);
                characterVisual.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                characterVisual.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
}