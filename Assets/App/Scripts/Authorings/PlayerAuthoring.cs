using App.Ecs;
using App.Ecs.Experience;
using App.Ecs.PlayerPerks;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private float initialDamageScale = 1;
        [SerializeField] private float initialFireScale = 1;
        [SerializeField] private float initialExpScale = 1;
        
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new PlayerTag());
                AddComponent(entity, new InitializeCameraTargetFlag());
                AddComponent(entity, new CameraTarget());
                
                AddComponent(entity, new DamageScale() { Value = authoring.initialDamageScale });
                AddComponent(entity, new FireRateScale() { Value = authoring.initialFireScale });
                AddComponent(entity, new ExpScale() { Value = authoring.initialExpScale });
            }
        }
    }
}