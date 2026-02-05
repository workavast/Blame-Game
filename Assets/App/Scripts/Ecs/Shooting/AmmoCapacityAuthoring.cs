using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Shooting
{
    public class AmmoCapacityAuthoring : MonoBehaviour
    {
        [SerializeField] private int ammoCapacity;

        private class Baker : Baker<AmmoCapacityAuthoring>
        {
            public override void Bake(AmmoCapacityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AmmoCapacity
                {
                    DefaultValue = authoring.ammoCapacity,
                    Value = authoring.ammoCapacity
                });
            }
        }
    }
}