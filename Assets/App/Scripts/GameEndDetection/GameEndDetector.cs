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
        
        private void OnDestroy()
        {
            if (_gameIsOver) 
                _gamePause.SetPauseState(false);
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
            if (_gameIsOver)
            {
                Debug.LogError("Game Already overed");
                return;
            }
            
            _gameIsOver = true;
            gameWinUi.Show();
            _gamePause.SetPauseState(true);
        }
        
        private void GameLoose()
        {
            if (_gameIsOver)
            {
                Debug.LogError("Game Already overed");
                return;
            }
            
            _gameIsOver = true;
            gameLooseUi.Show();
            _gamePause.SetPauseState(true);
        }
    }
}