using Unity.Entities;

namespace App.Ecs.Turrets.Deployment
{
    public class TurretInitialStateDeploymentAuthoring : TurretInitialStateAuthoringBase
    {
        private class Baker : Baker<TurretInitialStateDeploymentAuthoring>
        {
            public override void Bake(TurretInitialStateDeploymentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new TurretStateDeploymentTag());
            }
        }
    }
}