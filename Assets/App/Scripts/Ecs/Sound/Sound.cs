using App.Ecs.SystemGroups;
using Unity.Collections;
using Unity.Entities;

namespace App.Ecs.Sound
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(InitOffSystemGroup))]
    public partial class SfxStartLoadSystemGroup : ComponentSystemGroup
    {
        
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SfxStartLoadSystemGroup))]
    public partial class SfxSetSystemGroup : ComponentSystemGroup
    {
        
    }
    
    [UpdateInGroup(typeof(SfxStartLoadSystemGroup))]
    public abstract partial class SfxStartLoadSystem<TSfxData, TSfxLoadStartedTag, TSfxCleanup, TSfxCleanupTag> : SystemBase
        where TSfxData : unmanaged, IComponentData
        where TSfxLoadStartedTag : unmanaged, IComponentData
        where TSfxCleanup : unmanaged, ICleanupComponentData
        where TSfxCleanupTag : unmanaged, IComponentData
    {
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadWrite<TSfxData>(),
                ComponentType.Exclude<TSfxLoadStartedTag>()
            );
            
            RequireForUpdate(query);
            RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        protected override void OnUpdate()
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(EntityManager.WorldUnmanaged);
            
            var query = GetEntityQuery(
                ComponentType.ReadWrite<TSfxData>(),
                ComponentType.Exclude<TSfxLoadStartedTag>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var datas  = query.ToComponentDataArray<TSfxData>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++)
            {
                StartLoading(datas[i]);
                ecb.AddComponent(entities[i], new TSfxLoadStartedTag());
                ecb.AddComponent(entities[i], CreateSfxCleanup(datas[i]));
                ecb.AddComponent(entities[i], new TSfxCleanupTag());
            }
        }

        protected abstract void StartLoading(TSfxData sfxData);
        
        protected abstract TSfxCleanup CreateSfxCleanup(TSfxData sfxData);
    }
    
    [UpdateInGroup(typeof(SfxStartLoadSystemGroup))]
    public abstract partial class SfxCleanupSystem<TSfxCleanup, TSfxCleanupTag> : SystemBase
        where TSfxCleanup : unmanaged, ICleanupComponentData
        where TSfxCleanupTag : unmanaged, IComponentData
    {
        protected override void OnCreate()
        {
            var query = GetEntityQuery(
                ComponentType.ReadWrite<TSfxCleanup>(),
                ComponentType.Exclude<TSfxCleanupTag>()
            );
            
            RequireForUpdate(query);
            RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        protected override void OnUpdate()
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(EntityManager.WorldUnmanaged);
            
            var query = GetEntityQuery(
                ComponentType.ReadWrite<TSfxCleanup>(),
                ComponentType.Exclude<TSfxCleanupTag>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var cleanups  = query.ToComponentDataArray<TSfxCleanup>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++) 
                Release(cleanups[i]);
            
            ecb.RemoveComponent<TSfxCleanup>(entities);
        }

        protected abstract void Release(TSfxCleanup sfxData);
    }
    
    [UpdateInGroup(typeof(SfxSetSystemGroup))]
    public abstract partial class SfxSetSystem<TViewHolder, TSfxData, TSfxSetedTag> : SystemBase
        where TViewHolder : unmanaged, IComponentData
        where TSfxData : unmanaged, IComponentData
        where TSfxSetedTag : unmanaged, IComponentData
    {
        private EntityQuery _query;
        
        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                ComponentType.ReadWrite<TViewHolder>(),
                ComponentType.ReadOnly<TSfxData>(),
                ComponentType.Exclude<TSfxSetedTag>()
            );
            
            RequireForUpdate(_query);
            RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(WorldUpdateAllocator);

            var entities = _query.ToEntityArray(Allocator.Temp);
            var viewHolders  = _query.ToComponentDataArray<TViewHolder>(Allocator.Temp);
            var sfxDatas  = _query.ToComponentDataArray<TSfxData>(Allocator.Temp);

            for (var i = 0; i < entities.Length; i++)
            {
                ecb.AddComponent(entities[i], new TSfxSetedTag());
                var viewHolder = viewHolders[i];
                var sfxData = sfxDatas[i];
                SetData(viewHolder, sfxData);
            }
            
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }

        protected abstract void SetData(TViewHolder  viewHolder, TSfxData sfx);
    }
}