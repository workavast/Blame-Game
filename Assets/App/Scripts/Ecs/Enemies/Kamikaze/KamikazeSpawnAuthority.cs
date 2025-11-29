using App.Ecs.Enemies.Spawning;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.Kamikaze
{
    [RequireComponent(typeof(EnemySpawnerAuthority))]
    public class KamikazeSpawnAuthority : MonoBehaviour
    {
        private class Baker : Baker<KamikazeSpawnAuthority>
        {
            public override void Bake(KamikazeSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new KamikazeSpawnerTag());
            }
        }
    }
}