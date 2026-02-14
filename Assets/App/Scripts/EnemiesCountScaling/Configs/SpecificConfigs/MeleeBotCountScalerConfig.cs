using App.Ecs.Enemies.MeleeBot;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(MeleeBotCountScalerConfig), menuName = EnemiesCountScalingConsts.Path + nameof(MeleeBotCountScalerConfig))]
    public class MeleeBotCountScalerConfig : EnemiesCountScalerConfig
    {
        public override IEnemiesScaler TakeEnemiesScaler() 
            => new EnemiesCountScaler<MeleeBotSpawnerTag>(this);
    }
}