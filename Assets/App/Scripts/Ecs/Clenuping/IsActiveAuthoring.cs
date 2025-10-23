using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Clenuping
{
    public class IsActiveAuthoring : MonoBehaviour
    {
        private class Baker : Baker<IsActiveAuthoring>
        {
            public override void Bake(IsActiveAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new CleanUpTag());
            }
        }
    }
}