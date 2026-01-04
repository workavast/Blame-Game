using Unity.Entities;
using UnityEngine;

namespace App.Ecs.HealthOrbs.Dropping
{
    public class HealthDropGlobalDataAuthoring : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float dropChance;
        
        private class Baker : Baker<HealthDropGlobalDataAuthoring>
        {
            public override void Bake(HealthDropGlobalDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new HealthDropGlobalDataTag());
                AddComponent(entity, new HealthOrbDropChance { Value = authoring.dropChance });
            }
        }
    }
}