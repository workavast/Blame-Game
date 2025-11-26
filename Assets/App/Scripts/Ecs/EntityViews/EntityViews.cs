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
                    if (prefabRef.LoadingStatus != ObjectLoadingStatus.Completed
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Loading
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Queued)
                    {
                        var viewHolder = EntityManager.GetComponentData<EntityViewPrefabHolder>(entity);
                        viewHolder.Loaded = true;
                        ecb.SetComponent(entity, viewHolder);
                        
                        prefabRef.LoadAsync();
                    }

                    if (prefabRef.LoadingStatus == ObjectLoadingStatus.Completed)
                    {
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
                            var viewHolder = EntityManager.GetComponentData<EntityViewPrefabHolder>(entity);
                            viewHolder.Loaded = true;
                            ecb.SetComponent(entity, viewHolder); 
                        }
                        
                        ecb.SetComponentEnabled<EntityViewPrefabHolder>(entity, false);
                    }
                }
            }
            
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
    
    [UpdateInGroup(typeof(ViewsInitializationSystemGroup))]
    [UpdateAfter(typeof(EntityViewInstallerSystem))]
    public abstract partial class ViewHolderInitializeSystem<TInitializeFlag, TView, TViewHolder> : SystemBase
        where TInitializeFlag : unmanaged, IComponentData
        where TView : MonoBehaviour, IEntityViewElement
        where TViewHolder : unmanaged, IComponentData
    {
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewHolder>(),
                ComponentType.ReadOnly<TInitializeFlag>(),
                ComponentType.Exclude<TViewHolder>()
            );
            
            RequireForUpdate(query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);

            var query = GetEntityQuery(
                ComponentType.ReadWrite<EntityViewHolder>(),
                ComponentType.ReadOnly<TInitializeFlag>(),
                ComponentType.Exclude<TViewHolder>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var viewHolders  = query.ToComponentDataArray<EntityViewHolder>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var view = viewHolders[i].Instance.Value.GetView<TView>();
                
                AddViewHolder(ref ecb, entity, view);
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }

        protected abstract void AddViewHolder(ref EntityCommandBuffer ecb, Entity entity, TView view);
    }
}