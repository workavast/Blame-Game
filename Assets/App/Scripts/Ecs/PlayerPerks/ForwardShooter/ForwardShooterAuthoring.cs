using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.ForwardShooter
{
    public class ForwardShooterAuthoring : MonoBehaviour
    {
        private class Baker : Baker<ForwardShooterAuthoring>
        {
            public override void Bake(ForwardShooterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ForwardShooterTag());
            }
        }
    }
}