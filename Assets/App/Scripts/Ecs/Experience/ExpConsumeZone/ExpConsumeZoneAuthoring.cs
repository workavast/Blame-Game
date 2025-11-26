using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience.ExpConsumeZone
{
    public class ExpConsumeZoneAuthoring : MonoBehaviour
    {
        private class Baker : Baker<ExpConsumeZoneAuthoring>
        {
            public override void Bake(ExpConsumeZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new ExpConsumeZoneTag());
            }
        }
    }
}