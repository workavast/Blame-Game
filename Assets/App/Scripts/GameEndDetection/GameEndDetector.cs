using App.GamePausing;
using App.GameTiming;
using App.PlayerProviding;
using App.ResourcesSystem.ForRun;
using App.ResourcesSystem.ResourcesValues;
using App.ResourcesSystem.Saves;
using App.ResourcesSystem.Storage;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.GameEndDetection
{
    public class GameEndDetector : MonoBehaviour
    {
        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly IGameTimerRO _gameTimer;
        [Inject] private readonly GamePause _gamePause;
        [Inject] private readonly ResourcesForRunProvider _resourcesStorageForRun;
        [Inject] private readonly ResourcesSaveManager _resourcesSaveManager;

        private bool _gameIsOver;
        
        private void Awake()
        {
            _playerProvider.OnPlayerDied += GameLoose;
        }
        
        private void OnDestroy()
        {
            if (_gameIsOver) 
                _gamePause.SetPauseState(false);
            else
                _resourcesSaveManager.Save();
        }

        private void Update()
        {
            if (_gameIsOver)
                return;
            
            if (_gameTimer.TimeIsOver) 
                GameWin();
        }

        private void GameWin()
        {
            if (_gameIsOver)
            {
                Debug.LogError("Game already ended");
                return;
            }
            
            _gameIsOver = true;
            
            _resourcesStorageForRun.GameEnded(true);
            _resourcesSaveManager.Save();

            _screensController.SetScreen<GameWinUi>();
            _gamePause.SetPauseState(true);
        }
        
        private void GameLoose()
        {
            if (_gameIsOver)
            {
                Debug.LogError("Game already ended");
                return;
            }
            
            _gameIsOver = true;
            
            _resourcesSaveManager.Save();
            
            _screensController.SetScreen<GameLooseUi>();
            _gamePause.SetPauseState(true);
        }
    }
}