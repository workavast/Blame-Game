using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.DamageZone
{
    public class DamageZoneAuthoring : MonoBehaviour
    {
        private class Baker : Baker<DamageZoneAuthoring>
        {
            public override void Bake(DamageZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new DamageZoneTag());
            }
        }
    }
}