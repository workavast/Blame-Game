using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.MeleeBot
{
    public class MeleeBotAuthoring : MonoBehaviour
    {
        private class Baker : Baker<MeleeBotAuthoring>
        {
            public override void Bake(MeleeBotAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new MeleeBotTag());
            }
        }
    }
}