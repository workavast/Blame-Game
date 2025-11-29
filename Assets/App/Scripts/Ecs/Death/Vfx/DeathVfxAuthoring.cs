using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Death.Vfx
{
    [RequireComponent(typeof(DeathViewRequestAuthoring))]
    public class DeathVfxAuthoring : MonoBehaviour
    {
        private class Baker : Baker<DeathVfxAuthoring>
        {
            public override void Bake(DeathVfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new DeathVfxViewHolderInitializeFlag());
            }
        }
    }
}