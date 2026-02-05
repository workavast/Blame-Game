using Unity.Entities;

namespace App.Ecs.Turrets.ReadyToUse
{
    public class TurretInitialStateReadyToUseAuthoring : TurretInitialStateAuthoringBase
    {
        private class Baker : Baker<TurretInitialStateReadyToUseAuthoring>
        {
            public override void Bake(TurretInitialStateReadyToUseAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
 
                AddComponent(entity, new TurretStateReadyToUseTag());
            }
        }
    }
}