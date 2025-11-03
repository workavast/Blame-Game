using System.Threading.Tasks;
using App.Audio.Ambience;
using App.Localization;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private StringTablesPreloader stringTablesPreloader;
        [SerializeField] private AmbienceBootstrap ambienceBootstrap;
        [SerializeField] private int delayBeforeHideLoadingScreenInMilliseconds = 1000;
        
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private async void Start()
        {
            await stringTablesPreloader.Preload();
            
            ambienceBootstrap.Initialize();

            var initialLoading = _sceneLoader.PrevTargetSceneIndex <= -1;
            if (!initialLoading)
                await AwaitLagOnSceneLoading();
            
            _sceneLoader.HideLoadScreen(initialLoading);
        }

        private void OnDestroy()
        {
            stringTablesPreloader.Release();
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