using App.Ecs;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Authorings
{
    public class KamikazeSpawnAuthority : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float interval;
        [SerializeField] private float distance;
        [SerializeField] private uint seed;

        private class Baker : Baker<KamikazeSpawnAuthority>
        {
            public override void Bake(KamikazeSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new KamikazeSpawnData()
                {
                    Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    Distance =  authoring.distance,
                    Interval = authoring.interval
                });
                AddComponent(entity, new KamikazeSpawner()
                {
                    Timer = 0f,
                    Random = Random.CreateFromIndex(authoring.seed)
                });
            }
        }
    }

}