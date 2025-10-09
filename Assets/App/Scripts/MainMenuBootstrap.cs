using App.GamePausing;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [Inject] private readonly GamePause _gamePause;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Start()
        {
            _gamePause.SetPauseState(false);
            _sceneLoader.HideLoadScreen(false);
        }
    }
}