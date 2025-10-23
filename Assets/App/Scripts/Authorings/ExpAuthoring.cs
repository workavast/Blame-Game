using App.Ecs;
using App.Ecs.Experience;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class ExpAuthoring : MonoBehaviour
    {
        [SerializeField] private ExpOrbAuthoring expOrbPrefab;
        [SerializeField] private float impulse;
        [SerializeField] private float consumeAcceleration;
        [SerializeField] private float consumeMoveSpeed;
        [SerializeField] private float expOrbVerticalOffset;
        [SerializeField] private float expOrbConsumeDistanceError;
 
        private class Baker : Baker<ExpAuthoring>
        {
            public override void Bake(ExpAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new ExpTag());
                AddComponent(entity, new PlayerExp());
                AddComponent(entity, new ExpOrbPrefabHolder() { OrbPrefab = GetEntity(authoring.expOrbPrefab, TransformUsageFlags.Dynamic) });
                AddComponent(entity, new ExpOrbDropImpulse() { Value = authoring.impulse });
                AddComponent(entity, new ExpOrbDropHeight() { Value = authoring.expOrbVerticalOffset });
                AddComponent(entity, new ExpOrbConsumeDistanceError() { Value = authoring.expOrbConsumeDistanceError });
                AddComponent(entity, new ExpOrbConsumeMoveSpeed()
                {
                    MoveSpeed = authoring.consumeMoveSpeed,
                    Acceleration = authoring.consumeAcceleration
                });
                
                AddBuffer<ExpOrbsDropRequest>(entity);
            }
        }
    }
}