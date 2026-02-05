using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Turrets.ReadyToUse
{
    public class TurretCapacityViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<TurretCapacityViewAuthoring>
        {
            public override void Bake(TurretCapacityViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
 
                AddComponent(entity, new TurretCapacityViewOwnerTag());
            }
        }
    }
}