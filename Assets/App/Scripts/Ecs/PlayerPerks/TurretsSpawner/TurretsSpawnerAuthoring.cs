using App.Ecs.Shooting;
using App.Ecs.Turrets;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.TurretsSpawner
{
    public class TurretsSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private TurretAuthoring turretPrefab;
        [SerializeField] private int turretsCount;
        [SerializeField] private float height;
        [SerializeField] private float minDropImpulse;
        [SerializeField] private float maxDropImpulse;
        private class Baker : Baker<TurretsSpawnerAuthoring>
        {
            public override void Bake(TurretsSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AdditionalProjectilesCount());
                
                AddComponent(entity, new TurretsSpawnerTag());
                AddComponent(entity, new TurretsSpawnerData()
                {
                    TurretPrefab = GetEntity(authoring.turretPrefab, TransformUsageFlags.Dynamic),
                    TurretsCount = authoring.turretsCount,
                    Height = authoring.height,
                    MinDropImpulse = authoring.minDropImpulse,
                    MaxDropImpulse = authoring.maxDropImpulse
                });
            }
        }
    }
}