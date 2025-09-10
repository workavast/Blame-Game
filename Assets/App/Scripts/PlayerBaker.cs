using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace App.Scripts
{
    public struct PlayerTag : IComponentData
    {
    }

    public struct CameraTarget : IComponentData
    {
        public UnityObjectRef<Transform> CameraTransform;
    }

    public struct InitializeCameraTargetFlag : IComponentData { }
    
    public class PlayerBaker : MonoBehaviour
    {
        private class Baker : Baker<PlayerBaker>
        {
            public override void Bake(PlayerBaker authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTag());
                AddComponent(entity, new InitializeCameraTargetFlag());
                AddComponent(entity, new CameraTarget());
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CameraInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializeCameraTargetFlag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var target = CameraSingleton.Instance.CameraTarget;

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

    [UpdateAfter(typeof(TransformSystemGroup))]
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
    
    public partial class PlayerInputSystem : SystemBase
    {
        private readonly InputSystem_Actions _input = new();
        
        protected override void OnCreate()
        {
            _input.Enable();
        }

        protected override void OnUpdate()
        {
            var moveDirectionInput = (float2)_input.Player.Move.ReadValue<Vector2>();
            foreach (var moveDirection in SystemAPI.Query<RefRW<MoveDirection>>().WithAll<PlayerTag>())
            {
                moveDirection.ValueRW.Value = moveDirectionInput;
            }
        }
    }
}