using System.Threading;
using System.Threading.Tasks;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class HideLoadingScreenBootstrap : Bootstrap
    {
        [SerializeField] private bool hideInstantly;
        [Inject] private readonly ISceneLoader _sceneLoader;

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            _sceneLoader.HideLoadScreen(hideInstantly);
            return Task.CompletedTask;
        }
    }
}