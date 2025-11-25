using App.Audio.Sources;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Rockets
{
    public struct RocketSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> SfxPrefab;
    }
    
    public partial class RockSfxStartLoadSystem : SfxStartLoadSystem<RocketSfxData>
    {
        protected override void StartLoading(RocketSfxData comp)
        {
            comp.SfxPrefab.LoadAsync();
        }
    }
    
    [UpdateInGroup(typeof(SfxSetSystemGroup))]
    public partial struct RocketSfxSetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (viewHolder, sfx, entity)  in 
                     SystemAPI.Query<RefRO<RocketViewHolder>, RefRO<RocketSfxData>>()
                         .WithAll<RocketTag>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetSfxView(sfx.ValueRO.SfxPrefab);
            }
        }
    }
}