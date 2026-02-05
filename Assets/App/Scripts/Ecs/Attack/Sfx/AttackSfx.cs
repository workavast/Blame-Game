using App.Audio.Sources;
using App.Ecs.EntityViews;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Transforms;

namespace App.Ecs.Attack.Sfx
{
    public struct AttackSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> AttackSfxRef;
    }

    public struct AttackSfxViewHolder : IComponentData
    {
        public UnityObjectRef<AttackSfxView> Instance;
    }

    public struct AttackSfxViewOwnerTag : IComponentData
    {

    }

    public struct AttackSfxLoadStartedTag : IComponentData
    {

    }

    public struct AttackSfxSetedTag : IComponentData
    {

    }

    public struct AttackSfxCleanup : ICleanupComponentData
    {
        public WeakObjectReference<AudioPoolRelease> SfxRef;
    }

    public struct AttackSfxCleanupTag : IComponentData
    {

    }

    public partial class AttackSfxViewHolderInitSystem
        : ViewHolderInitializeSystem<AttackSfxViewOwnerTag, AttackSfxView, AttackSfxViewHolder>
    {
        protected override AttackSfxViewHolder CreateViewHolder(AttackSfxView view)
            => new() { Instance = view };
    }

    public partial class AttackSfxStartLoadingSystem : SfxStartLoadSystem<AttackSfxData, AttackSfxLoadStartedTag,
        AttackSfxCleanup, AttackSfxCleanupTag>
    {
        protected override void StartLoading(AttackSfxData sfxData)
            => sfxData.AttackSfxRef.LoadAsync();

        protected override AttackSfxCleanup CreateSfxCleanup(AttackSfxData sfxData)
            => new() { SfxRef = sfxData.AttackSfxRef };
    }

    public partial class AttackSfxCleanupSystem : SfxCleanupSystem<AttackSfxCleanup, AttackSfxCleanupTag>
    {
        protected override void Release(AttackSfxCleanup sfxData)
            => sfxData.SfxRef.Release();
    }

    public partial class AttackSfxSetSystem : SfxSetSystem<AttackSfxViewHolder, AttackSfxData, AttackSfxSetedTag>
    {
        protected override void SetData(AttackSfxViewHolder viewHolder, AttackSfxData sfx)
            => viewHolder.Instance.Value.SetSfxRef(sfx.AttackSfxRef);
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackSfxActivateAtViewSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<AttackSfxViewHolder, AttackViewRequested>()
                .Build();

            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackSfx, _) in
                     SystemAPI.Query<RefRW<AttackSfxViewHolder>, EnabledRefRO<AttackViewRequested>>()
                         .WithNone<Owner>())
            {
                attackSfx.ValueRW.Instance.Value.Activate();
            }
        }
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackSfxActivateAtOwnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<AttackSfxViewHolder, AttackViewRequested>()
                .Build();

            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackSfx, owner, _) in
                     SystemAPI.Query<RefRW<AttackSfxViewHolder>, RefRO<Owner>, EnabledRefRO<AttackViewRequested>>())
            {
                var ownerPosition = SystemAPI.GetComponentRO<LocalToWorld>(owner.ValueRO.Value);
                attackSfx.ValueRW.Instance.Value.Activate(ownerPosition.ValueRO.Position);
            }
        }
    }
}