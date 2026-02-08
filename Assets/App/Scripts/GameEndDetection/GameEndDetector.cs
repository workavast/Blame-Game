using App.GamePausing;
using App.GameTiming;
using App.PlayerProviding;
using App.ResourcesSystem.ResourcesValues;
using App.ResourcesSystem.Storage;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.GameEndDetection
{
    public class GameEndDetector : MonoBehaviour
    {
        [SerializeField] private ResourcesValueConfig resourcesForWin;
        
        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly IGameTimerRO _gameTimer;
        [Inject] private readonly GamePause _gamePause;
        [Inject] private readonly ResourcesStorage _resourcesStorage;

        private bool _gameIsOver;
        
        private void Awake()
        {
            _playerProvider.OnPlayerDied += GameLoose;
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
            
            if (_gameTimer.TimeIsOver) 
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
            _resourcesStorage.Add(resourcesForWin.ResourcesAmount);
            _screensController.ToggleScreen<GameWinUi>(true);
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
            _screensController.ToggleScreen<GameLooseUi>(true);
            _gamePause.SetPauseState(true);
        }
    }
}