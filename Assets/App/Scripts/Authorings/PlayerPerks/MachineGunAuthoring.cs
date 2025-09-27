using App.Ecs;
using App.Ecs.PlayerPerks;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings.PlayerPerks
{
    public class MachineGunAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float spawnVerticalOffset;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float shootPause;
        [SerializeField] private float distanceReaction;
        
        private class Baker : Baker<MachineGunAuthoring>
        {
            public override void Bake(MachineGunAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new MachineGunTag());
                AddComponent(entity, new MachineGunDistanceReactionData(){Value = authoring.distanceReaction});
                AddComponent(entity, new MachineGunPause()
                {
                    Timer = authoring.shootPause
                });
                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    SpawnVerticalOffset = authoring.spawnVerticalOffset,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    ShootPause =  authoring.shootPause,
                });
            }
        }
    }
}