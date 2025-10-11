using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    public abstract  class EnemiesCountScalerConfig : ScriptableObject
    {
        [SerializeField] private AnimationCurve countPerMinute;

        public float GetCountPerSecond(float timeInMinutes)
            => countPerMinute.Evaluate(timeInMinutes) / 60;
        
        public abstract IEnemiesScaler TakeEnemiesScaler();
    }
}