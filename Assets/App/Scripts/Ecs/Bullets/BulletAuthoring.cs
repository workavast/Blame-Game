using App.Ecs.Attack;
using App.Ecs.Moving;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Bullets
{
    public class BulletAuthoring : MonoBehaviour
    {
        private class Baker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new BulletTag());
                AddComponent(entity, new AttackDamage());
                AddComponent(entity, new BulletPenetration());
                AddBuffer<BulletCollisions>(entity);

                AddComponent(entity, new MoveSpeed());
            }
        }
    }
}