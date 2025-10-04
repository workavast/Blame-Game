using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject[] authoringPrefabs;
        
        private class Baker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                var buffer = AddBuffer<SpawnCell>(entity);

                var prefabs = authoring.authoringPrefabs;
                if (prefabs == null)
                    return;
                for (var i = 0; i < prefabs.Length; i++)
                {
                    if (prefabs[i] == null)
                        continue;
                    
                    buffer.Add(new SpawnCell()
                    {
                        Key = prefabs[i].name.GetHashCode(),
                        Prefab = GetEntity(prefabs[i], TransformUsageFlags.Dynamic)
                    });
                }
            }
        }
    }
}