using System;
using App.Ecs.Sound;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.EntityViews
{
    public struct EntityViewPrefabHolder : IComponentData, IEnableableComponent
    {
        public WeakObjectReference<EntityView> Prefab;
        public bool Loaded;
    }
    
    public struct EntityViewHolder : IComponentData
    {
        public UnityObjectRef<EntityView> Instance;
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SfxStartLoadSystemGroup))]
    public partial class ViewsInitializationSystemGroup : ComponentSystemGroup
    {
        
    }
    
    [UpdateInGroup(typeof(ViewsInitializationSystemGroup))]
    public sealed partial class EntityViewInstallerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewPrefabHolder>(),
                ComponentType.Exclude<EntityViewHolder>()
            );
            
            RequireForUpdate(query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);

            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewPrefabHolder>(),
                ComponentType.Exclude<EntityViewHolder>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var prefabHolders  = query.ToComponentDataArray<EntityViewPrefabHolder>(Allocator.Temp);

            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var prefabRef = prefabHolders[i].Prefab;

                if (prefabRef.IsReferenceValid)
                {
                    switch (prefabRef.LoadingStatus)
                    {
                        case ObjectLoadingStatus.None:
                            var viewHolderNone = EntityManager.GetComponentData<EntityViewPrefabHolder>(entity);
                            viewHolderNone.Loaded = true;
                            ecb.SetComponent(entity, viewHolderNone);
                            prefabRef.LoadAsync();
                            break;

                        case ObjectLoadingStatus.Completed:
                            var instance = ServicesBridge.Get<SpawnProvider>().Spawn(prefabRef.Result);
                            instance.SetPrefab(ref prefabRef);

                            ecb.AddComponent(entity, new EntityViewHolder { Instance = instance });
                            ecb.AddComponent(entity, new CleanupCallbackHolder()
                            {
                                Instance = instance.CleanupCallback,
                            });

                            if (!prefabHolders[i].Loaded)
                            {
                                prefabRef.LoadAsync();
                                var viewHolderCompleted =
                                    EntityManager.GetComponentData<EntityViewPrefabHolder>(entity);
                                viewHolderCompleted.Loaded = true;
                                ecb.SetComponent(entity, viewHolderCompleted);
                            }

                            ecb.SetComponentEnabled<EntityViewPrefabHolder>(entity, false);
                            break;

                        case ObjectLoadingStatus.Error:
                            var viewHolderError = EntityManager.GetComponentData<EntityViewPrefabHolder>(entity);
                            viewHolderError.Loaded = true;
                            ecb.SetComponent(entity, viewHolderError);
                            ecb.SetComponentEnabled<EntityViewPrefabHolder>(entity, false);
                            Debug.LogError($"Some error with loading asset. Entity index: {entity.Index}");
                            break;
                    }
                }
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
    
    [UpdateInGroup(typeof(ViewsInitializationSystemGroup))]
    [UpdateAfter(typeof(EntityViewInstallerSystem))]
    public abstract partial class ViewHolderInitializeSystem<TTag, TView, TViewHolder> : SystemBase
        where TTag : unmanaged, IComponentData
        where TView : MonoBehaviour, IEntityViewElement
        where TViewHolder : unmanaged, IComponentData
    {
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewHolder>(),
                ComponentType.ReadOnly<TTag>(),
                ComponentType.Exclude<TViewHolder>()
            );
            
            RequireForUpdate(query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);

            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewHolder>(),
                ComponentType.ReadOnly<TTag>(),
                ComponentType.Exclude<TViewHolder>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var viewHolders  = query.ToComponentDataArray<EntityViewHolder>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var view = viewHolders[i].Instance.Value.GetView<TView>();
                
                ecb.AddComponent(entity, CreateViewHolder(view));
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }

        protected abstract TViewHolder CreateViewHolder(TView view);
    }
}