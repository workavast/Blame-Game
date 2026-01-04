using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Experience
{
    public class ExpGlobalDataAuthoring : MonoBehaviour
    {
        private class Baker : Baker<ExpGlobalDataAuthoring>
        {
            public override void Bake(ExpGlobalDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new ExpGlobalDataTag());
                AddComponent(entity, new PlayerExp());
            }
        }
    }
}