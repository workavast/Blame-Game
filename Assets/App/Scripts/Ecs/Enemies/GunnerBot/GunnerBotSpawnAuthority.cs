using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.GunnerBot
{
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