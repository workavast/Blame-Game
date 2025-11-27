using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Characters
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new LookPoint());
                AddComponent(entity, new RotationSpeed() { Value = authoring.rotationSpeed });
                
                AddComponent(entity, new CharacterTag());
            }
        }
    }
}