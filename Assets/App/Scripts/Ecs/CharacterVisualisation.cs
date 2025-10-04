using App.Views;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
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
    
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct PhysicsCharacterVisualisationUpdateSystem : ISystem
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
            foreach (var (transform, physicsVelocity, characterVisual) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<PhysicsVelocity>, RefRW<CharacterViewHolder>>())
            {
                characterVisual.ValueRO.Instance.Value.SetVelocity(physicsVelocity.ValueRO.Linear);
                characterVisual.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                characterVisual.ValueRO.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
    
    public partial struct CharacterVisualisationUpdateSystem : ISystem
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