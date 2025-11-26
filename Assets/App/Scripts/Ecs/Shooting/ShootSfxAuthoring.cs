using App.Audio.Sources;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Shooting
{
    public class ShootSfxAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<AudioPoolRelease> shootSfxRef;

        private class Baker : Baker<ShootSfxAuthoring>
        {
            public override void Bake(ShootSfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ShooterSfxTag());
                AddComponent(entity, new ShooterSfxDataHolder() { ShootSfxRef = authoring.shootSfxRef });
            }
        }
    }
}