using App.Ecs.Enemies;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new EnemyTag());
            }
        }
    }
}