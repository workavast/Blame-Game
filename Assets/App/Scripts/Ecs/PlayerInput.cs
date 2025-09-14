using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs
{
    public partial class PlayerInputSystem : SystemBase
    {
        private readonly InputSystem_Actions _input = new();
        
        protected override void OnCreate()
        {
            _input.Enable();
        }

        protected override void OnUpdate()
        {
            var playerInput = ServiceLocator.Get<InputProvider>();
            var moveDirectionInput = (float2)playerInput.Input.Player.Move.ReadValue<Vector2>();
            var lookPointInput = (float3)playerInput.LookPoint;

            foreach (var (moveDirection, lookPoint) in SystemAPI.Query<RefRW<MoveDirection>, RefRW<LookPoint>>().WithAll<PlayerTag>())
            {
                moveDirection.ValueRW.Value = moveDirectionInput;
                lookPoint.ValueRW.Value = lookPointInput;
            }
        }
    }
}