using App.Ecs;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<EntityView> entityViewPrefab;
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PhysicsMassInitializeFlag());
                AddComponent(entity, new MoveDirection());
                AddComponent(entity, new MoveSpeed() { Value = authoring.moveSpeed });
                
                AddComponent(entity, new CharacterVisualPrefab()
                {
                    Prefab = authoring.entityViewPrefab
                });
            }
        }
    }
}