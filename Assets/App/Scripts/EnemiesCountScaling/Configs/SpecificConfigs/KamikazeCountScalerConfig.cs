using App.Ecs.Enemies;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(KamikazeCountScalerConfig), menuName = "App/" + nameof(KamikazeCountScalerConfig))]
    public class KamikazeCountScalerConfig : EnemiesCountScalerConfig
    {
        public override IEnemiesScaler TakeEnemiesScaler() 
            => new EnemiesCountScaler<KamikazeSpawnerTag>(this);
    }
}