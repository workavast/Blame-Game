using System.Threading.Tasks;
using App.GamePausing;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private int delayBeforeHideLoadingScreenInMilliseconds = 1000;
        
        [Inject] private readonly GamePause _gamePause;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private async void Start()
        {
            _gamePause.SetPauseState(false);

            var initialLoading = _sceneLoader.PrevTargetSceneIndex <= -1;
            if (!initialLoading)
                await AwaitLagOnSceneLoading();
            
            _sceneLoader.HideLoadScreen(initialLoading);
        }

        /// <summary>
        /// On scene loading can happened lag, that skip some time of fade process of loading screen,
        /// so we wait some time to skip this lag
        /// </summary>
        /// <remarks>Lag still can be visible in editor, cus in editor lag bigger then build</remarks>
        private async Task AwaitLagOnSceneLoading()
        {
            await Task.Delay(delayBeforeHideLoadingScreenInMilliseconds);
        }
    }
}