using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack.Cooldown
{
    public class AttackCooldownAuthoring : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private bool haveRateScale;
        
        private class Baker : Baker<AttackCooldownAuthoring>
        {
            public override void Bake(AttackCooldownAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new DefaultAttackCooldown() { Timer = authoring.attackCooldown });
                AddComponent(entity, new AttackCooldown() { Timer = authoring.attackCooldown });
                
                if (authoring.haveRateScale)
                    AddComponent(entity, new AttackRateScale());
            }
        }
    }
}