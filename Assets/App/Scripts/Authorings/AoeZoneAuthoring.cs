using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class AoeZoneAuthoring : MonoBehaviour
    {
        [SerializeField] private float initialRadius;
        [SerializeField] private float targetRadius;
        [SerializeField] private float radiusFactor;
        
        private class Baker : Baker<AoeZoneAuthoring>
        {
            public override void Bake(AoeZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new AoeZoneTag());
                AddComponent(entity, new AoeZoneRadius() { Value = authoring.initialRadius });
                AddComponent(entity, new AoeZoneTargetRadius() { Value = authoring.targetRadius });
                AddComponent(entity, new AoeZoneRadiusFactor() { Value = authoring.radiusFactor });
            }
        }
    }
}