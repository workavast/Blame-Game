using App.Ecs.Enemies;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(GunnerBotCountScalerConfig), menuName = "App/" + nameof(GunnerBotCountScalerConfig))]
    public class GunnerBotCountScalerConfig : EnemiesCountScalerConfig
    {
        public override IEnemiesScaler TakeEnemiesScaler() 
            => new EnemiesCountScaler<GunnerBotSpawnerTag>(this);
    }
}