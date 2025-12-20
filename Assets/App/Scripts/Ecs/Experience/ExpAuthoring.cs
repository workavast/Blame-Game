using App.Ecs.Experience.ExpDropping;
using App.Ecs.Experience.ExpOrb;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience
{
    public class ExpAuthoring : MonoBehaviour
    {
        [SerializeField] private ExpOrbAuthoring expOrbPrefab;
        [SerializeField] private float impulse;
        [SerializeField] private float expOrbVerticalOffset;
 
        private class Baker : Baker<ExpAuthoring>
        {
            public override void Bake(ExpAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new ExpGlobalDataTag());
                AddComponent(entity, new PlayerExp());
                AddComponent(entity, new ExpOrbPrefabHolder() { OrbPrefab = GetEntity(authoring.expOrbPrefab, TransformUsageFlags.Dynamic) });
                AddComponent(entity, new ExpOrbDropImpulse() { Value = authoring.impulse });
                AddComponent(entity, new ExpOrbDropHeight() { Value = authoring.expOrbVerticalOffset });
                
                AddBuffer<ExpOrbsDropRequest>(entity);
            }
        }
    }
}