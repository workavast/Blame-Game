using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Turrets
{
    public class TurretAuthoring : MonoBehaviour
    {
        [SerializeField] private float distanceReaction = 10;
        [SerializeField] private int capacity = 10;

        private class Baker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new TurretTag());
                AddComponent(entity, new ShootDistanceReaction() { Value = authoring.distanceReaction });
                AddComponent(entity, new TurretCapacity()
                {
                    DefaultValue = authoring.capacity,
                    Value = authoring.capacity
                });
            }
        }
    }
}