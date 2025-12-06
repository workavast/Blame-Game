using System.Threading;
using System.Threading.Tasks;
using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class AutoSceneLoadingBootstrap : Bootstrap
    {
        [SerializeField] private SceneReference sceneForLoading;
        [SerializeField] private bool showLoadingScreenInstantly = true;
        
        [Inject] private readonly ISceneLoader _sceneLoader;

        protected override Task SelfInitialization(CancellationToken cancellationToken) 
        {
            _sceneLoader.LoadScene(sceneForLoading.SceneIndex, showLoadingScreenInstantly);
            
            return Task.CompletedTask;
        }
    }
}