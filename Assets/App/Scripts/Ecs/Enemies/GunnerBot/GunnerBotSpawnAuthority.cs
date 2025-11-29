using App.Ecs.Enemies.Spawning;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.GunnerBot
{
    [RequireComponent(typeof(EnemySpawnerAuthority))]
    public class GunnerBotSpawnAuthority : MonoBehaviour
    {
        private class Baker : Baker<GunnerBotSpawnAuthority>
        {
            public override void Bake(GunnerBotSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new GunnerBotSpawnerTag());
            }
        }
    }
}