using System.Threading.Tasks;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class AwaitBootstrap : Bootstrap
    {
        [SerializeField] private int awaitDelay = 1000;

        [Inject] private readonly ISceneLoader _sceneLoader;

        protected override Task SelfInitialization()
        {
            var initialLoading = _sceneLoader.PrevTargetSceneIndex <= -1;
            if (!initialLoading)
                return Await();
            else
                return Task.CompletedTask;
        }
        
        private async Task Await() 
            => await Task.Delay(awaitDelay);
    }
}
