using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack.Vfx
{
    public class AttackVfxAuthoring : MonoBehaviour
    {
        private class Baker : Baker<AttackVfxAuthoring>
        {
            public override void Bake(AttackVfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AttackVfxViewHolderInitializeFlag());
            }
        }
    }
}