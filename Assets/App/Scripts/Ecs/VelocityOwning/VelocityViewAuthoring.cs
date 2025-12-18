using Unity.Entities;
using UnityEngine;

namespace App.Ecs.VelocityOwning
{
    public class VelocityViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<VelocityViewAuthoring>
        {
            public override void Bake(VelocityViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new VelocityViewOwner());
            }
        }
    }
}