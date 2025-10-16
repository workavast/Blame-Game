using App.Ecs.Clenuping;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs
{
    public struct ViewPrefabHolder : IComponentData, IEnableableComponent
    {
        public WeakObjectReference<CleanupView> Prefab;
        public bool Loaded;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class ViewInstallSystemGroup : ComponentSystemGroup
    {
        
    }
    
    [UpdateInGroup(typeof(ViewInstallSystemGroup))]
    public abstract partial class ViewInstallerSystem<TTag> : SystemBase
        where TTag : unmanaged, IComponentData
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                ComponentType.ReadWrite<ViewPrefabHolder>(),
                ComponentType.ReadOnly<TTag>()
            );
            
            RequireForUpdate(_query);
        }
        
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);
            
            var query = GetEntityQuery(
                ComponentType.ReadWrite<ViewPrefabHolder>(),
                ComponentType.ReadOnly<TTag>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var holders  = query.ToComponentDataArray<ViewPrefabHolder>(Allocator.Temp);
            
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var prefabRef = holders[i].Prefab;
                
                if (prefabRef.IsReferenceValid)
                {
                    if (prefabRef.LoadingStatus != ObjectLoadingStatus.Completed
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Loading
                        && prefabRef.LoadingStatus != ObjectLoadingStatus.Queued)
                    {
                        var viewHolder = EntityManager.GetComponentData<ViewPrefabHolder>(entity);
                        viewHolder.Loaded = true;
                        ecb.SetComponent(entity, viewHolder);
                        
                        prefabRef.LoadAsync();
                    }

                    if (prefabRef.LoadingStatus == ObjectLoadingStatus.Completed)
                    {
                        var instance = ServicesBridge.Get<SpawnProvider>().Spawn(prefabRef.Result);

                        instance.SetPrefab(ref prefabRef);
                        
                        AddViewHolder(entity, instance, ref ecb);
                        ecb.AddComponent(entity, new Clenuping.CleanupCallback()
                        {
                            Instance = instance.CleanupCallback, 
                        });

                        if (!holders[i].Loaded)
                        {
                            prefabRef.LoadAsync();
                            var viewHolder = EntityManager.GetComponentData<ViewPrefabHolder>(entity);
                            viewHolder.Loaded = true;
                            ecb.SetComponent(entity, viewHolder); 
                        }
                        
                        ecb.SetComponentEnabled<ViewPrefabHolder>(entity, false);
                    }
                }
            }
            
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }

        protected abstract void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb);
    }
}