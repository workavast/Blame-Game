using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Attack
{
    public class AttackDamageAuthoring : MonoBehaviour
    {
        [SerializeField] private float defaultDamage;
        [SerializeField] private float defaultDamageScale = 1;
        
        private class Baker : Baker<AttackDamageAuthoring>
        {
            public override void Bake(AttackDamageAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new AttackDamage()
                {
                    Value = authoring.defaultDamage,
                    Scale = authoring.defaultDamageScale
                });
            }
        }
    }
}