using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Spawning
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject[] authoringPrefabs = {};
        
        private class Baker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new SpawnerTag());
                AddBuffer<SpawnRequest>(entity);
                var prefabsBuffer = AddBuffer<PrefabCell>(entity);
                
                var prefabs = authoring.authoringPrefabs;
                for (var i = 0; i < prefabs.Length; i++)
                {
                    if (prefabs[i] == null)
                        continue;
                    
                    prefabsBuffer.Add(new PrefabCell()
                    {
                        Key = prefabs[i].name.GetHashCode(),
                        Prefab = GetEntity(prefabs[i], TransformUsageFlags.Dynamic)
                    });
                }
            }
        }
    }
}