using App.Ecs.Enemies;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Authorings.Enemies
{
    public class GunnerBotSpawnAuthority : MonoBehaviour
    {
        [SerializeField] private GunnerBotAuthoring prefab;
        [SerializeField] private float interval;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<GunnerBotSpawnAuthority>
        {
            public override void Bake(GunnerBotSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new GunnerBotSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new GunnerBotSpawner()
                {
                    Timer = 0f,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }
}