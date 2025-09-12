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
            var moveDirectionInput = (float2)_input.Player.Move.ReadValue<Vector2>();
            foreach (var moveDirection in SystemAPI.Query<RefRW<MoveDirection>>().WithAll<PlayerTag>())
            {
                moveDirection.ValueRW.Value = moveDirectionInput;
            }
        }
    }
}