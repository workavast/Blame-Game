using App.Ecs.Attack;
using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Bullets
{
    public class BulletOwnerAuthoring : MonoBehaviour
    {
        [SerializeField] private BulletAuthoring bulletPrefab;
        [Header("Default Values")]
        [SerializeField] private float damage;
        [SerializeField] private int penetration;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float spawnVerticalOffset;
        [Header("Scales")]
        [SerializeField] private bool hasDamageScale;
        [SerializeField] private bool hasPenetrationScale;
        
        private class Baker : Baker<BulletOwnerAuthoring>
        {
            public override void Bake(BulletOwnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    SpawnVerticalOffset = authoring.spawnVerticalOffset,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    Penetration = authoring.penetration
                });

                if (authoring.hasDamageScale)
                    AddComponent(entity, new AttackDamageScale());

                if (authoring.hasPenetrationScale)
                    AddComponent(entity, new AdditionalPenetration());
            }
        }
    }
}