using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs.Characters
{
    public struct CharacterTag : IComponentData
    {

    }

    public struct CharacterViewHolder : IComponentData
    {
        public UnityObjectRef<CharacterView> Instance;
    }

    public partial class CharacterViewHolderInitSystem
        : ViewHolderInitializeSystem<CharacterTag, CharacterView, CharacterViewHolder>
    {
        protected override CharacterViewHolder CreateViewHolder(CharacterView view)
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct PhysicsCharacterViewUpdateSystem : ISystem
    {
        private EntityQuery _query;

        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, PhysicsVelocity, CharacterViewHolder>()
                .Build();

            state.RequireForUpdate(_query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, physicsVelocity, characterViewHolder) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<PhysicsVelocity>, RefRW<CharacterViewHolder>>())
            {
                characterViewHolder.ValueRW.Instance.Value.SetVelocity(physicsVelocity.ValueRO.Linear);
                characterViewHolder.ValueRW.Instance.Value.SetPositionAndRotation(transform.ValueRO.Position, transform.ValueRO.Rotation);
            }
        }
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct CharacterViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, CharacterViewHolder>()
                .WithNone<PhysicsVelocity>()
                .Build();

            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, characterViewHolder) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CharacterViewHolder>>()
                         .WithNone<PhysicsVelocity>())
            {
                characterViewHolder.ValueRW.Instance.Value.SetVelocity(float3.zero);
                characterViewHolder.ValueRW.Instance.Value.SetPositionAndRotation(transform.ValueRO.Position, transform.ValueRO.Rotation);
            }
        }
    }
}