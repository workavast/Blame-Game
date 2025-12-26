using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Orbs
{
    public class OrbsDropAuthoring : MonoBehaviour
    {
        [SerializeField] private OrbAuthoring orbPrefab;
        [SerializeField] private float height;
        [SerializeField] private float impulse;
        
        private class Baker : Baker<OrbsDropAuthoring>
        {
            public override void Bake(OrbsDropAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new OrbsDropGlobalDataTag());
                AddComponent(entity, new OrbPrefabHolder { Prefab = GetEntity(authoring.orbPrefab, TransformUsageFlags.None) });
                AddComponent(entity, new OrbDropHeight { Value = authoring.height });
                AddComponent(entity, new OrbDropImpulse { Value = authoring.impulse });
                
                AddBuffer<OrbsDropRequest>(entity);
            }
        }
    }
}