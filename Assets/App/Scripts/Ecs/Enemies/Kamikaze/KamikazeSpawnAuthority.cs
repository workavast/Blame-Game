using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.Kamikaze
{
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