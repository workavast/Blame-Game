using App.GamePausing;
using App.GameTiming;
using App.PlayerProviding;
using UnityEngine;
using Zenject;

namespace App.GameEndDetection
{
    public class GameEndDetector : MonoBehaviour
    {
        [SerializeField] private GameWinUi gameWinUi;
        [SerializeField] private GameLooseUi gameLooseUi;
        [SerializeField] private float gameTimeToWin; 

        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly IGameTimerRO _gameTimer;
        [Inject] private readonly GamePause _gamePause;

        private bool _gameIsOver;
        
        private void Awake()
        {
            _playerProvider.OnPlayerDied += GameLoose;
            gameLooseUi.Hide();
            gameWinUi.Hide();
        }

        private void Update()
        {
            if (_gameIsOver)
                return;
            
            if (_gameTimer.Time >= gameTimeToWin) 
                GameWin();
        }

        private void GameWin()
        {
            _gameIsOver = true;
            gameWinUi.Show();
            _gamePause.SetPauseState(true);
        }
        
        private void GameLoose()
        {
            _gameIsOver = true;
            gameLooseUi.Show();
            _gamePause.SetPauseState(true);
        }
    }
}