using App.Audio.Sources;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Rockets
{
    public struct RocketSfxLoadStartedTag : IComponentData
    {
        
    }
    
    public struct RocketSfxSetedTag : IComponentData
    {
        
    }
    
    public struct RocketSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> SfxPrefab;
    }
    
    public partial class RockSfxStartLoadSystem : Sound.SfxStartLoadSystem<RocketSfxData, RocketSfxLoadStartedTag>
    {
        protected override void StartLoading(RocketSfxData comp) 
            => comp.SfxPrefab.LoadAsync();
    }
    
    public partial class RocketSfxSetSystem : SfxSetSystem<RocketViewHolder, RocketSfxData, RocketSfxSetedTag>
    {
        protected override void SetData(RocketViewHolder viewHolder, RocketSfxData sfx) 
            => viewHolder.Instance.Value.SetSfxView(sfx.SfxPrefab);
    }
}