using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Orbs
{
    public class OrbGlobalAuthoring : MonoBehaviour
    {
        [SerializeField] private float consumeAcceleration;
        [SerializeField] private float consumeMoveSpeed;
        [SerializeField] private float expOrbConsumeDistanceError;
 
        private class Baker : Baker<OrbGlobalAuthoring>
        {
            public override void Bake(OrbGlobalAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new OrbGlobalDataTag());
                AddComponent(entity, new OrbConsumeDistanceError() { Value = authoring.expOrbConsumeDistanceError });
                AddComponent(entity, new OrbConsumeMoveSpeed()
                {
                    MoveSpeed = authoring.consumeMoveSpeed,
                    Acceleration = authoring.consumeAcceleration
                });
            }
        }
    }
}