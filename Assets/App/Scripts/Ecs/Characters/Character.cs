using App.Audio.Sources;
using App.Ecs.Clenuping;
using App.Ecs.Sound;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs.Characters
{
    public struct CharacterTag : IComponentData
    {
        
    }

    public struct CharacterSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> DeathSfxRef;
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
    
    public partial class CharacterSfxInitializer : SfxInitializeSystem<CharacterSfxData, CharacterTag>
    {
        protected override void StartLoading(CharacterSfxData sfxData)
        {
            sfxData.DeathSfxRef.LoadAsync();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CharacterSfxSetSystem : ISystem
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
                     SystemAPI.Query<RefRO<CharacterViewHolder>, RefRO<CharacterSfxData>>()
                         .WithAll<CharacterTag>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetDeathSfx(sfx.ValueRO.DeathSfxRef);
            }
        }
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