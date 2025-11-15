using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack
{
    public class AttackViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<AttackViewAuthoring>
        {
            public override void Bake(AttackViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AttackViewInitializeFlag());
                AddComponent(entity, new AttackViewHolderInitializeFlag());
                AddComponent(entity, new AttackViewActivateFlag());
            }
        }
    }
}