using App.Ecs.EntityViews;
using Unity.Entities;

namespace App.Ecs.Health.Death.Vfx
{
    public struct DeathVfxViewOwnerTag : IComponentData
    {

    }

    public struct DeathVfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathVfxView> Instance;
    }

    public partial class DeathVfxViewHolderInitSystem
        : ViewHolderInitializeSystem<DeathVfxViewOwnerTag, DeathVfxView, DeathVfxViewHolder>
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
                .WithAll<DeathVfxViewHolder, DeathFlag>()
                .Build();
            
            state.RequireForUpdate(query);
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathView, _) in
                     SystemAPI.Query<RefRW<DeathVfxViewHolder>, EnabledRefRO<DeathFlag>>())
            {
                deathView.ValueRW.Instance.Value.Activate();
            }
        }
    }
}