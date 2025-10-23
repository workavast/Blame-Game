using App.Ecs.Enemies.MeleeBot;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(MeleeBotCountScalerConfig), menuName = "App/" + nameof(MeleeBotCountScalerConfig))]
    public class MeleeBotCountScalerConfig : EnemiesCountScalerConfig
    {
        public override IEnemiesScaler TakeEnemiesScaler() 
            => new EnemiesCountScaler<MeleeBotSpawnerTag>(this);
    }
}