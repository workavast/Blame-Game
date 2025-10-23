using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.MeleeBot
{
    public class MeleeBotAuthoring : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float attackCooldown;
        
        private class Baker : Baker<MeleeBotAuthoring>
        {
            public override void Bake(MeleeBotAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new MeleeBotTag());
                AddComponent(entity, new AttackDamage() { Value = authoring.damage });
                AddComponent(entity, new DefaultAttackCooldown() { Timer = authoring.attackCooldown });
                AddComponent(entity, new AttackCooldown() { Timer = authoring.attackCooldown });
                AddComponent(entity, new AutoMoveTag());
            }
        }
    }
}