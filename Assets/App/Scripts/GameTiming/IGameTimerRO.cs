namespace App.GameTiming
{
    public interface IGameTimerRO
    {
        public float Time { get; }
        public float Minutes { get; }
        public float Seconds { get; }
    }
}