using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Turrets.Deployment
{
    public class TurretDeploymentAuthoring : MonoBehaviour
    {
        [SerializeField] public float deploymentTime = 1f;

        private class Baker : Baker<TurretDeploymentAuthoring>
        {
            public override void Bake(TurretDeploymentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new TurretDeploymentTimer() { TargetValue = authoring.deploymentTime });
            }
        }
    }
}