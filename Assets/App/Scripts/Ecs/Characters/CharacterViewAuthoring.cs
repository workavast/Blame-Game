using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Characters
{
    public class CharacterViewAuthoring : MonoBehaviour
    {
        private class Baker : Baker<CharacterViewAuthoring>
        {
            public override void Bake(CharacterViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new CharacterTag());
            }
        }
    }
}