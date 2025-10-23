using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.GunnerBot
{
    public class GunnerBotSpawnAuthority : MonoBehaviour
    {
        [SerializeField] private GunnerBotAuthoring prefab;
        [SerializeField] private float interval = 1;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<GunnerBotSpawnAuthority>
        {
            public override void Bake(GunnerBotSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new GunnerBotSpawnerTag());
                AddComponent(entity, new EnemiesSpawnCountPerSecond());
                AddComponent(entity, new GunnerBotSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new GunnerBotSpawner()
                {
                    Timer = authoring.interval,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }
}