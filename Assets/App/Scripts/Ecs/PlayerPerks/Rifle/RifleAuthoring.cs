using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.Rifle
{
    public class RifleAuthoring : MonoBehaviour
    {
        [SerializeField] private float distanceReaction;

        private class Baker : Baker<RifleAuthoring>
        {
            public override void Bake(RifleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new RifleTag());
                AddComponent(entity, new ShootDistanceReaction() { Value = authoring.distanceReaction });
            }
        }
    }
}