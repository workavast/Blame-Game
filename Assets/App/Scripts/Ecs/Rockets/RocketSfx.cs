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

    public struct RocketSfxCleanup : ICleanupComponentData
    {
        public WeakObjectReference<AudioPoolRelease> SfxRef;
    }

    public struct RocketSfxCleanupTag : IComponentData
    {

    }

    public partial class RockSfxStartLoadSystem : SfxStartLoadSystem<RocketSfxData, RocketSfxLoadStartedTag,
        RocketSfxCleanup, RocketSfxCleanupTag>
    {
        protected override void StartLoading(RocketSfxData comp)
            => comp.SfxPrefab.LoadAsync();

        protected override RocketSfxCleanup CreateSfxCleanup(RocketSfxData sfxData)
            => new() { SfxRef = sfxData.SfxPrefab };
    }

    public partial class RocketSfxCleanupSystem : SfxCleanupSystem<RocketSfxCleanup, RocketSfxCleanupTag>
    {
        protected override void Release(RocketSfxCleanup sfxData)
            => sfxData.SfxRef.Release();
    }

    public partial class RocketSfxSetSystem : SfxSetSystem<RocketViewHolder, RocketSfxData, RocketSfxSetedTag>
    {
        protected override void SetData(RocketViewHolder viewHolder, RocketSfxData sfx)
            => viewHolder.Instance.Value.SetSfxView(sfx.SfxPrefab);
    }
}