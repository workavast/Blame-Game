using App.Ecs.Clenuping;
using Unity.Mathematics;
using UnityEngine;

namespace App.Ecs.AoeZones
{
    public class AoeZoneView : CleanupView
    {
        public void SetPosition(float3 position) 
            => SetPosition((Vector3)position);

        public void SetPosition(Vector3 position) 
            => transform.position = position;

        public void SetRadius(float radius)
        {
            var scale = transform.localScale;
            scale.x = scale.z = 2 * radius;
            transform.localScale = scale;
        }
    }
}