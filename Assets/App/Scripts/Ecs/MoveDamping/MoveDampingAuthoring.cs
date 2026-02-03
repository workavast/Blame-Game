using Unity.Entities;
using UnityEngine;

namespace App.Ecs.MoveDamping
{
    public class MoveDampingAuthoring : MonoBehaviour
    {
        [SerializeField] private bool enabledByDefault = true;
        [SerializeField] private float damping = 0.5f;
        [SerializeField] private float dampingScale = 10f;
        [SerializeField] private float dampingScaleMoveSpeedLimit = 1f;

        private class Baker : Baker<MoveDampingAuthoring>
        {
            public override void Bake(MoveDampingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                if (authoring.enabledByDefault)
                    AddComponent(entity, new MoveDampingTag());
                
                AddComponent(entity, new InertialMoveDamping
                {
                    BaseValue = authoring.damping,
                    Scale = authoring.dampingScale,
                    ScaleMoveSpeedLimit = authoring.dampingScaleMoveSpeedLimit
                });
            }
        }
    }
}