using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Turrets.Deployment
{
    public class TurretDeploymentViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<TurretDeploymentViewAuthoring>
        {
            public override void Bake(TurretDeploymentViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new TurretDeploymentViewOwnerTag());
            }
        }
    }
}