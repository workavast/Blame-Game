using Unity.Mathematics;
using UnityEngine;

namespace App
{
    public class EntityView : MonoBehaviour
    {
        public float Velocity { get; private set; }
        
        public void SetVelocity(float3 velocity)
        {
            Velocity = ((Vector3)velocity).magnitude;
        }
        
        public void SetVelocity(float velocity)
        {
            Velocity = velocity;
        }

        public void SetPosition(float3 position)
        {
            // transform.Translate((Vector3)position - transform.position);
            transform.position = position;
        }
        
        public void SetRotation(quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}