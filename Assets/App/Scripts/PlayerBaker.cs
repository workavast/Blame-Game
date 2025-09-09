using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace App.Scripts
{
    public struct PlayerTag : IComponentData { }

    public class PlayerBaker : MonoBehaviour
    {
        private class Baker : Baker<PlayerBaker>
        {
            public override void Bake(PlayerBaker authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTag());
            }
        }
    }
    
    public partial class PlayerInputSystem : SystemBase
    {
        private InputSystem_Actions _input;
        
        protected override void OnCreate()
        {
            _input = new InputSystem_Actions();
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