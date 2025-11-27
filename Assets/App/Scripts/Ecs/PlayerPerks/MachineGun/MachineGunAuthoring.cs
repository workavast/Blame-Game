using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.MachineGun
{
    public class MachineGunAuthoring : MonoBehaviour
    {
        [SerializeField] private float distanceReaction;

        private class Baker : Baker<MachineGunAuthoring>
        {
            public override void Bake(MachineGunAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new MachineGunTag());
                AddComponent(entity, new ShootDistanceReaction() { Value = authoring.distanceReaction });
            }
        }
    }
}