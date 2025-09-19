using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs
{
    public struct CameraTarget : IComponentData
    {
        public UnityObjectRef<Transform> CameraTransform;
    }

    public struct InitializeCameraTargetFlag : IComponentData { }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CameraInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializeCameraTargetFlag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var target = ServiceLocator.Get<CameraTargetProvider>().CameraTarget;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (cameraTarget, entity) in 
                     SystemAPI.Query<RefRW<CameraTarget>>()
                         .WithAll<InitializeCameraTargetFlag, PlayerTag>()
                         .WithEntityAccess())
            {
                cameraTarget.ValueRW.CameraTransform = target;
                ecb.RemoveComponent<InitializeCameraTargetFlag>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
    
    [UpdateAfter(typeof(PhysicsCharacterVisualisationUpdateSystem))]
    public partial class MoveCameraSystem : SystemBase 
    {
        protected override void OnUpdate()
        {
            foreach (var (transform, cameraTarget) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CameraTarget>>()
                         .WithAll<PlayerTag>()
                         .WithNone<InitializeCameraTargetFlag>())
            {
                cameraTarget.ValueRW.CameraTransform.Value.position = transform.ValueRO.Position;
            }
        }
    }
}