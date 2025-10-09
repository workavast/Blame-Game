using App.GamePausing.EcsPausing;
using UnityEngine;

namespace App.GamePausing
{
    public class GamePause
    {
        private readonly EcsPause _ecsPause;
     
        private int _pauseRequestCount;
        
        public GamePause(EcsPause ecsPause)
        {
            _ecsPause = ecsPause;
        }

        public void SetPauseState(bool isPause)
        {
            if (isPause)
                _pauseRequestCount++;
            else
                _pauseRequestCount--;

            if (_pauseRequestCount > 1)
                return;

            if (_pauseRequestCount < 0)
            {
                _pauseRequestCount = 0;
                Debug.LogWarning("Ypu try unset pause game when it already unpaused");
                return;
            }
            
            _ecsPause.SetPauseState(isPause);
            Time.timeScale = isPause ? 0 : 1;
        }
    }
}