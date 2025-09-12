using App.Ecs;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerTag());
                AddComponent(entity, new InitializeCameraTargetFlag());
                AddComponent(entity, new CameraTarget());
            }
        }
    }
}