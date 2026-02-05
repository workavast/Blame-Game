using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Health.Death.Vfx
{
    [RequireComponent(typeof(HealthAuthoring))]
    public class DeathVfxAuthoring : MonoBehaviour
    {
        private class Baker : Baker<DeathVfxAuthoring>
        {
            public override void Bake(DeathVfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DeathVfxViewOwnerTag());
            }
        }
    }
}