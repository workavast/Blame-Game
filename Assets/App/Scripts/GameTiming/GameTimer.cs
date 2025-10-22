namespace App.GameTiming
{
    public class GameTimer : IGameTimerRO
    {
        public float Time { get; private set; }
        public float Minutes { get; private set; }
        public float Seconds { get; private set; }

        public GameTimer(float initialTimeInSeconds)
        {
            IncreaseTime(initialTimeInSeconds);
        }
        
        public void IncreaseTime(float deltaTime)
        {
            Time += deltaTime;
            
            Minutes = Time / 60;
            Seconds = Time % 60;
        }
    }
}