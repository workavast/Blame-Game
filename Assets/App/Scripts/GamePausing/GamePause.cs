using App.GamePausing.EcsPausing;
using UnityEngine;

namespace App.GamePausing
{
    public class GamePause
    {
        private readonly RequestCounter _requestCounter;
        private readonly EcsPause _ecsPause;
        
        public GamePause(EcsPause ecsPause)
        {
            _requestCounter = new RequestCounter(ApplyPauseState);
            _ecsPause = ecsPause;
        }

        public void SetPauseState(bool isPause) 
            => _requestCounter.ChangeRequests(isPause);

        private void ApplyPauseState(bool pause)
        {
            _ecsPause.SetPauseState(pause);
            Time.timeScale = pause ? 0 : 1;  
        }
    }
}