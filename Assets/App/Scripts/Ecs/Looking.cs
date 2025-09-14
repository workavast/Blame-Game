using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs
{
    public struct LookPoint : IComponentData
    {
        public float3 Value;
    }

    public struct RotationSpeed : IComponentData
    {
        public float Value;
    }

    public partial struct LookAtPointSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, lookPoint, rotationSpeed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<LookPoint>, RefRO<RotationSpeed>>())
            {
                var position = transform.ValueRO.Position;
                var lookDirection = lookPoint.ValueRO.Value - position;
                lookDirection.y = 0;
                
                if (math.lengthsq(lookDirection) < 0.0001f)
                    continue;

                var targetRot = quaternion.LookRotationSafe(lookDirection, math.up());

                transform.ValueRW.Rotation = RotateTowards(
                    transform.ValueRO.Rotation,
                    targetRot,
                    rotationSpeed.ValueRO.Value * deltaTime
                );
            }
        }
        
        private static quaternion RotateTowards(quaternion from, quaternion to, float maxRadiansDelta)
        {
            var cosTheta = math.dot(from.value, to.value);
            
            Debug.Log(cosTheta);
            // if rotations almost same, return target
            if (cosTheta > 0.999999f)
                return to;

            // Take shortest way
            if (cosTheta < 0f)
            {
                to.value = -to.value;
                cosTheta = -cosTheta;
            }

            var angleDifference = math.acos(math.clamp(cosTheta, -1f, 1f));
            var t = math.min(1f, maxRadiansDelta / angleDifference);

            return math.slerp(from, to, t);
        }
    }
}