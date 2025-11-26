using App.Ecs.SystemGroups;
using Unity.Collections;
using Unity.Entities;

namespace App.Ecs.Sound
{
    public struct SfxLoadStartedTag : IComponentData
    {
        
    }
    
    public struct SfxInitedTag : IComponentData
    {
        
    }
    
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
    public abstract partial class SfxStartLoadSystem<TSfxData> : SystemBase
        where TSfxData : unmanaged, IComponentData
    {
        protected override void OnCreate()
        {
            RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        protected override void OnUpdate()
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(EntityManager.WorldUnmanaged);
            
            var query = GetEntityQuery(
                ComponentType.ReadWrite<TSfxData>(),
                ComponentType.Exclude<SfxLoadStartedTag>()
            );
            
            var entities = query.ToEntityArray(Allocator.Temp);
            var holders  = query.ToComponentDataArray<TSfxData>(Allocator.Temp);
            
            for (var i = 0; i < entities.Length; i++)
            {
                StartLoading(holders[i]);
                ecb.AddComponent(entities[i], new SfxLoadStartedTag());
            }
        }

        protected abstract void StartLoading(TSfxData sfxData);
    }
}