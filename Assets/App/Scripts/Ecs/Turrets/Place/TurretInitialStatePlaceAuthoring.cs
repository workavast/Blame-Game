using Unity.Entities;

namespace App.Ecs.Turrets.Place
{
    public class TurretInitialStatePlaceAuthoring : TurretInitialStateAuthoringBase
    {
        private class Baker : Baker<TurretInitialStatePlaceAuthoring>
        {
            public override void Bake(TurretInitialStatePlaceAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new TurretStatePlaceTag());
            }
        }
    }
}