using App.Ecs.EntityViews;
using Unity.Entities;

namespace App.Ecs.Death.Vfx
{
    public struct DeathVfxViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathVfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathVfxView> Instance;
    }

    public partial class DeathVfxViewHolderInitSystem
        : ViewHolderInitializeSystem<DeathVfxViewHolderInitializeFlag, DeathVfxView, DeathVfxViewHolder>
    {
        protected override DeathVfxViewHolder CreateViewHolder(DeathVfxView view)
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct DeathVfxActivateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<DeathVfxViewHolder, DeathViewRequestedFlag>()
                .Build();
            
            state.RequireForUpdate(query);
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathView, _) in
                     SystemAPI.Query<RefRW<DeathVfxViewHolder>, EnabledRefRO<DeathViewRequestedFlag>>())
            {
                deathView.ValueRW.Instance.Value.Activate();
            }
        }
    }
}