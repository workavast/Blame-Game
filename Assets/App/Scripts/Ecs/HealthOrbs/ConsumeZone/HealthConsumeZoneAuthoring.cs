using Unity.Entities;
using UnityEngine;

namespace App.Ecs.HealthOrbs.ConsumeZone
{
    public class HealthConsumeZoneAuthoring : MonoBehaviour
    {
        private class Baker : Baker<HealthConsumeZoneAuthoring>
        {
            public override void Bake(HealthConsumeZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new HealthConsumeZoneTag());
            }
        }
    }
}