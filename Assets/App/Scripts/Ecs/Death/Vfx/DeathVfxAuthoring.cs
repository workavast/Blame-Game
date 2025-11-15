using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Death
{
    public class DeathVfxAuthoring : MonoBehaviour
    {
        private class Baker : Baker<DeathVfxAuthoring>
        {
            public override void Bake(DeathVfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DeathVfxViewInitializeFlag());
                AddComponent(entity, new DeathVfxViewHolderInitializeFlag());
                AddComponent(entity, new DeathVfxViewActivateFlag());
            }
        }
    }
}