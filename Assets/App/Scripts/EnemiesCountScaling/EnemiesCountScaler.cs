using App.Ecs.Enemies;
using App.EnemiesCountScaling.Configs;
using Unity.Entities;

namespace App.EnemiesCountScaling
{
    public class EnemiesCountScaler<TSpawnerTag> : IEnemiesScaler
        where TSpawnerTag : unmanaged, IComponentData
    {
        private readonly EnemiesCountScalerConfig _config;
        
        public EnemiesCountScaler(EnemiesCountScalerConfig config)
        {
            _config = config;
        }

        public void UpdateEnemiesScaling(float timeInMinutes)
        {
            var scale = _config.GetCountPerSecond(timeInMinutes);
            EcsSingletons.TrySetComponentOfSingleton<TSpawnerTag, EnemiesSpawnCountPerSecond>(new EnemiesSpawnCountPerSecond()
            {
                Value = scale
            });
        }
    }

    public interface IEnemiesScaler
    {
        public void UpdateEnemiesScaling(float timeInMinutes);
    }
}