using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Randomisation
{
    public class RandomAuthoring : MonoBehaviour
    {
        private class Baker : Baker<RandomAuthoring>
        {
            public override void Bake(RandomAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new RandomHolderRequiredInitializationFlag());
                AddComponent(entity, new RandomHolder());
            }
        }
    }
}