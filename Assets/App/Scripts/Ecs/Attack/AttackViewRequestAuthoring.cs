using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack
{
    public class AttackViewRequestAuthoring : MonoBehaviour
    {
        private class Baker : Baker<AttackViewRequestAuthoring>
        {
            public override void Bake(AttackViewRequestAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new AttackRequested());
                AddComponent(entity, new AttackInitRequired());
            }
        }
    }
}