using System.Collections.Generic;
using App.EnemiesCountScaling.Configs;
using App.GameTiming;

namespace App.EnemiesCountScaling
{
    public class EnemiesCountScalersHolder
    {
        private readonly IGameTimerRO _gameTimer;
        private readonly List<IEnemiesScaler> _scalers;
        
        public EnemiesCountScalersHolder(IGameTimerRO gameTimer, EnemiesCountScalersConfig config)
        {
            _gameTimer = gameTimer;
            
            _scalers = new List<IEnemiesScaler>(config.Configs.Count);
            foreach (var scaleConfig in config.Configs) 
                _scalers.Add(scaleConfig.TakeEnemiesScaler());
        }

        public void UpdateScalers()
        {
            foreach (var scaler in _scalers) 
                scaler.UpdateEnemiesScaling(_gameTimer.Minutes);
        }
    }
}