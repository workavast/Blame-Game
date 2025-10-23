using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.Kamikaze
{
    public class KamikazeSpawnAuthority : MonoBehaviour
    {
        [SerializeField] private KamikazeAuthoring prefab;
        [SerializeField] private float interval = 1;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<KamikazeSpawnAuthority>
        {
            public override void Bake(KamikazeSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new KamikazeSpawnerTag());
                AddComponent(entity, new EnemiesSpawnCountPerSecond());
                AddComponent(entity, new KamikazeSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new KamikazeSpawner()
                {
                    Timer = authoring.interval,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }
}