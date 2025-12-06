using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Death
{
    public class DeathViewRequestAuthoring : MonoBehaviour
    {
        private class Baker : Baker<DeathViewRequestAuthoring>
        {
            public override void Bake(DeathViewRequestAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DeathViewRequestedFlag());
                AddComponent(entity, new DeathViewInitRequiredFlag());
            }
        }
    }
}