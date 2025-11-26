using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.MeleeBot
{
    public class MeleeBotSpawnAuthority : MonoBehaviour
    {
        private class Baker : Baker<MeleeBotSpawnAuthority>
        {
            public override void Bake(MeleeBotSpawnAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new MeleeBotSpawnerTag());
            }
        }
    }
}