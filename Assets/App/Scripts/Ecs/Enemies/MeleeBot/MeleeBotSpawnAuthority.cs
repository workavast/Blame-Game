using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.MeleeBot
{
    public class MeleeBotSpawnAuthority : MonoBehaviour
    {
        [SerializeField] private MeleeBotAuthoring prefab;
        [SerializeField] private float interval = 1;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<MeleeBotSpawnAuthority>
        {
            public override void Bake(MeleeBotSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new MeleeBotSpawnerTag());
                AddComponent(entity, new EnemiesSpawnCountPerSecond());
                AddComponent(entity, new MeleeBotSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new MeleeBotSpawner()
                {
                    Timer = authoring.interval,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }
}