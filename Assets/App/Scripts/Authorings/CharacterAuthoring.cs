using App.Ecs;
using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<CleanupView> entityViewPrefab;
        [SerializeField] private float health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new IsActiveTag());
                AddComponent(entity, new PhysicsMassInitializeFlag());
                
                AddComponent(entity, new MoveDirection());
                AddComponent(entity, new MoveSpeed() { Value = authoring.moveSpeed });
                
                AddComponent(entity, new LookPoint());
                AddComponent(entity, new RotationSpeed() {Value = authoring.rotationSpeed});
                
                AddComponent(entity, new MaxHealth(){Value = authoring.health});
                AddComponent(entity, new CurrentHealth(){Value = authoring.health});

                AddBuffer<DamageFrameBuffer>(entity);
                
                AddComponent(entity, new CharacterTag());
                AddComponent(entity, new ViewPrefabHolder()
                {
                    Prefab = authoring.entityViewPrefab
                });
            }
        }
    }
}