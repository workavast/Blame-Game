using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.Spawning
{
    public class EnemySpawnerAuthority : MonoBehaviour
    {
        [SerializeField] private EnemyAuthoring prefab;
        [SerializeField] private float interval = 1;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<EnemySpawnerAuthority>
        {
            public override void Bake(EnemySpawnerAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new EnemySpawnerTag());
                AddComponent(entity, new EnemySpawnCountPerSecond());
                AddComponent(entity, new EnemySpawnerSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new EnemySpawner()
                {
                    Timer = authoring.interval,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }
}