using App.Ecs;
using App.Ecs.Enemies;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings.Enemies
{
    public class KamikazeAuthoring : MonoBehaviour
    {
        [SerializeField] private float explosionDamage;
        
        private class KamikazeBaker : Baker<KamikazeAuthoring>
        {
            public override void Bake(KamikazeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new KamikazeTag());
                AddComponent(entity, new AttackDamage() { Value = authoring.explosionDamage });
            }
        }
    }
}