using App.Ecs.Enemies.GunnerBot;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(GunnerBotCountScalerConfig), menuName = EnemiesCountScalingConsts.Path + nameof(GunnerBotCountScalerConfig))]
    public class GunnerBotCountScalerConfig : EnemiesCountScalerConfig
    {
        public override IEnemiesScaler TakeEnemiesScaler() 
            => new EnemiesCountScaler<GunnerBotSpawnerTag>(this);
    }
}