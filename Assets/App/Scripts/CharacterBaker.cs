using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

namespace App.Scripts
{
    public struct InitializeFlag : IComponentData, IEnableableComponent { }
    
    public struct MoveDirection : IComponentData
    {
        public float2 Value;
    }

    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }

    public class CharacterBaker : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<CharacterBaker>
        {
            public override void Bake(CharacterBaker authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new InitializeFlag());
                AddComponent(entity, new MoveDirection());
                AddComponent(entity, new MoveSpeed(){Value = authoring.moveSpeed});
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InitializeSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsMass, flag) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<InitializeFlag>>())
            {
                physicsMass.ValueRW.InverseInertia = float3.zero;
                flag.ValueRW = false;
            }
        }
    }
    
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsVelocity,direction,speed) 
                     in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MoveDirection>, RefRO<MoveSpeed>>())
            {
                var step2D = direction.ValueRO.Value * speed.ValueRO.Value * Time.deltaTime;
                physicsVelocity.ValueRW.Linear += new float3(step2D.x, 0, step2D.y);
            }
        }
    }
}