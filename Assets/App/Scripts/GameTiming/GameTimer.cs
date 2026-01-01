using UnityEngine;

namespace App.GameTiming
{
    public class GameTimer : IGameTimerRO
    {
        public float Time { get; private set; }
        public float Minutes { get; private set; }
        public float Seconds { get; private set; }
        
        public float RemainTime { get; private set; }
        public float RemainMinutes { get; private set; }
        public float RemainSeconds { get; private set; }
        
        public bool TimeIsOver => RemainTime <= 0;

        private readonly float _winTime;

        public GameTimer(GameTimeConfig config)
        {
            _winTime = config.WinTime;
            IncreaseTime(config.StartTime);
        }
        
        public void IncreaseTime(float deltaTime)
        {
            Time += deltaTime;
            Minutes = Time / 60;
            Seconds = Time % 60;
            
            RemainTime = _winTime - Time;
            RemainMinutes = Mathf.Clamp(RemainTime / 60, 0, float.PositiveInfinity);
            RemainSeconds = Mathf.Clamp(RemainTime % 60, 0, float.PositiveInfinity);
        }
    }
}