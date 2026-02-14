using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Shooting.Ammo
{
    public class AmmoCapacityViewAuthoring : MonoBehaviour
    {
        [SerializeField] private bool isVisibleByDefault;
        
        private class Baker : Baker<AmmoCapacityViewAuthoring>
        {
            public override void Bake(AmmoCapacityViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AmmoCapacityViewOwnerTag());
                
                if (authoring.isVisibleByDefault)
                    AddComponent(entity, new AmmoCapacityViewIsVisibleTag());
            }
        }
    }
}