using Unity.Entities;
using Unity.Entities.Content;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs
{
    public struct CharacterVisualPrefab : IComponentData
    {
        public WeakObjectReference<EntityView> Prefab;
    }
    
    public struct CharacterVisual : ICleanupComponentData
    {
        public UnityObjectRef<EntityView> Value;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class CharacterVisualisationSpawnerSystem : SystemBase
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<CharacterVisualPrefab>()
                .WithNone<CharacterVisual>()
                .Build();
            
            state.RequireForUpdate(_query);
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

                    // можно подождать синхронно (для простоты, но не лучший вариант для продакшена)
                    // var prefab = prefabRef.WaitForCompletion();

                    if (prefabRef.LoadingStatus == ObjectLoadingStatus.Completed)
                    {
                        // var view = Object.Instantiate(prefabRef.Result);
                        var view = SpawnProvider.Spawn(prefabRef.Result);
                        ecb.AddComponent(entity, new CharacterVisual { Value = view });
                    }
                }
            }
            
            ecb.Playback(EntityManager);
        }
    } 
    
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct CharacterVisualisationUpdateSystem : ISystem
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
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<PhysicsVelocity>, RefRO<CharacterVisual>>())
            {
                characterVisual.ValueRO.Value.Value.SetVelocity(physicsVelocity.ValueRO.Linear);
                characterVisual.ValueRO.Value.Value.SetPosition(transform.ValueRO.Position);
                characterVisual.ValueRO.Value.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    } 
}