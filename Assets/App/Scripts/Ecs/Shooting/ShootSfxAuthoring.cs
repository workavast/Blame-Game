using App.Audio.Sources;
using App.Ecs.Clenuping;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Shooting
{
    public class ShootSfxAuthoring : MonoBehaviour
    {
        [SerializeField] private ShooterSfxView shooterSfxView;
        [SerializeField] private WeakObjectReference<AudioPoolRelease> shootSfxRef;

        private WeakObjectReference<CleanupView> _sfxViewPrefab;

        private class Baker : Baker<ShootSfxAuthoring>
        {
            public override void Bake(ShootSfxAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ShooterSfxTag());
                AddComponent(entity, new ViewPrefabHolder() { Prefab = authoring._sfxViewPrefab });
                AddComponent(entity, new ShooterSfxDataHolder() { ShootSfxRef = authoring.shootSfxRef });
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _sfxViewPrefab = new WeakObjectReference<CleanupView>(shooterSfxView);
        }
#endif
    }
}