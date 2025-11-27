using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.Kamikaze
{
    public class KamikazeAuthoring : MonoBehaviour
    {
        private class KamikazeBaker : Baker<KamikazeAuthoring>
        {
            public override void Bake(KamikazeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new KamikazeTag());
                AddComponent(entity, new AutoMoveTag());
            }
        }
    }
}