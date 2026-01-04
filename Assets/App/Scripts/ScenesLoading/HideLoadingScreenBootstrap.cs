using System.Threading;
using System.Threading.Tasks;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class HideLoadingScreenBootstrap : Bootstrap
    {
        [SerializeField] private LoadingConfig loadingConfig;

        [Inject] private readonly ISceneLoader _sceneLoader;

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            _sceneLoader.HideLoadScreen(loadingConfig.HideDuration);
            return Task.CompletedTask;
        }
    }
}