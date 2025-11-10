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
            => Await();

        private async Task Await() 
            => await Task.Delay(awaitDelay);
    }
}
