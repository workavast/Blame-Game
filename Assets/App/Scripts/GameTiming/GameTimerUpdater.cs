using UnityEngine;
using Zenject;

namespace App.GameTiming
{
    public class GameTimerUpdater : MonoBehaviour
    {
        [Inject] private readonly GameTimer _gameTimer;

        private void Update()
        {
            _gameTimer.IncreaseTime(Time.deltaTime);
        }
    }
}