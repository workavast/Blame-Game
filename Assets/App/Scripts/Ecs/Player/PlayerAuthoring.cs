using App.Ecs.Attack;
using App.Ecs.Attack.Cooldown;
using App.Ecs.Experience;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Player
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private float additionalDamageScale = 0;
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
                
                AddComponent(entity, new AttackDamage() { Scale = authoring.additionalDamageScale });
                AddComponent(entity, new AttackRateScale() { Value = authoring.initialFireScale });
                AddComponent(entity, new ExpScale() { Value = authoring.initialExpScale });
            }
        }
    }
}