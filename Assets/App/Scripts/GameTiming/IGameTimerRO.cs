namespace App.GameTiming
{
    public interface IGameTimerRO
    {
        public float Time { get; }
        public float Minutes { get; }
        public float Seconds { get; }
        
        public float RemainTime { get; }
        public float RemainMinutes { get; }
        public float RemainSeconds { get; }
        
        public bool TimeIsOver { get; }
    }
}