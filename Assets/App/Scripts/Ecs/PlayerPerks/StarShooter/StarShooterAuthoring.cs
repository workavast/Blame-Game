using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.StarShooter
{
    public class StarShooterAuthoring : MonoBehaviour
    {
        [SerializeField] private float bulletsCount;
        
        private class Baker : Baker<StarShooterAuthoring>
        {
            public override void Bake(StarShooterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new StarShooterTag());
                AddComponent(entity, new StarShooterData()
                {
                    BulletsCount = authoring.bulletsCount
                });
                
                AddComponent(entity, new AdditionalProjectilesCount());
            }
        }
    }
}