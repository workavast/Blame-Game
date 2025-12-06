using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Looking
{
    public class LookAuthoring : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private class Baker : Baker<LookAuthoring>
        {
            public override void Bake(LookAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new LookPoint());
                AddComponent(entity, new RotationSpeed() { Value = authoring.rotationSpeed });
            }
        }
    }
}