using App.Ecs.Clenuping;
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

    public partial class CharacterViewInstallerSystem : ViewInstallerSystem<CharacterTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new CharacterViewHolder { Instance = instance as CharacterView });
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
                characterViewHolder.ValueRO.Instance.Value.SetVelocity(physicsVelocity.ValueRO.Linear);
                characterViewHolder.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                characterViewHolder.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct CharacterViewUpdateSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<LocalToWorld, CharacterViewHolder>()
                .WithNone<PhysicsVelocity>()
                .Build();

            state.RequireForUpdate(_query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, characterVisual) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CharacterViewHolder>>()
                         .WithNone<PhysicsVelocity>())
            {
                characterVisual.ValueRO.Instance.Value.SetVelocity(float3.zero);
                characterVisual.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                characterVisual.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
}