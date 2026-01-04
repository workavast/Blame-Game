using Unity.Entities;
using UnityEngine;

namespace App.Ecs.HealthOrbs.Dropping
{
    public class HealthOrbDropperAuthoring : MonoBehaviour
    {
        [SerializeField] private int orbsCount;
        
        private class Baker : Baker<HealthOrbDropperAuthoring>
        {
            public override void Bake(HealthOrbDropperAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new HealthOrbDropper() { OrbsCount = authoring.orbsCount });
            }
        }
    }
}