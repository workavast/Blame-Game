using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class IsActiveAuthoring : MonoBehaviour
    {
        private class Baker : Baker<IsActiveAuthoring>
        {
            public override void Bake(IsActiveAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new IsActiveTag());
            }
        }
    }
}