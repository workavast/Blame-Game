using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack.Cooldown
{
    public class AttackCooldownViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<AttackCooldownViewAuthoring>
        {
            public override void Bake(AttackCooldownViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new AttackCooldownViewOwnerTag());
            }
        }
    }
}