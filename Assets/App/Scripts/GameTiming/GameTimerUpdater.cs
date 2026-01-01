using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimerUpdater : MonoBehaviour
    {
        [Inject] private readonly GameTimer _gameTimer;

        private bool _timerStarted;
        
        public void StartTimer()
        {
            _timerStarted = true;
        }
        
        private void Update()
        {
            if (!_timerStarted)
                return;
            
            _gameTimer.IncreaseTime(Time.deltaTime);
        }
    }
}