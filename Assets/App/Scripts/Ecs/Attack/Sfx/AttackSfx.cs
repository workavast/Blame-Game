using App.Audio.Sources;
using App.Ecs.EntityViews;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

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

    public partial class AttackSfxStartLoadingSystem : SfxStartLoadSystem<AttackSfxData>
    {
        protected override void StartLoading(AttackSfxData sfxData) 
            => sfxData.AttackSfxRef.LoadAsync();
    }

    public struct AttackSfxHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public partial class AttackSfxViewHolderInitSystem
        : ViewHolderInitializeSystem<AttackSfxHolderInitializeFlag, AttackSfxView, AttackSfxViewHolder>
    {
        protected override AttackSfxViewHolder CreateViewHolder(AttackSfxView view) 
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(SfxSetSystemGroup))]
    public partial struct AttackSfxSetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (viewHolder, sfx, entity) in
                     SystemAPI.Query<RefRO<AttackSfxViewHolder>, RefRO<AttackSfxData>>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetSfxRef(sfx.ValueRO.AttackSfxRef);
            }
        }
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackSfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackSfx, _) in
                     SystemAPI.Query<RefRO<AttackSfxViewHolder>, EnabledRefRO<AttackViewRequested>>())
            {
                attackSfx.ValueRO.Instance.Value.Activate();
            }
        }
    }
}