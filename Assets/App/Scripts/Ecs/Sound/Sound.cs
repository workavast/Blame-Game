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
    [UpdateAfter(typeof(ViewInstallSystemGroup))]
    public abstract partial class SfxInitializeSystem<TSfxData, TTag> : SystemBase
        where TSfxData : unmanaged, IComponentData
        where TTag : unmanaged, IComponentData
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
                ComponentType.ReadOnly<TTag>(),
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