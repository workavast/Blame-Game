using System.Threading.Tasks;
using Avastrad.ScenesLoading;
using Zenject;

namespace App.Bootstraps
{
    public class LoadTargetSceneBootstrap : Bootstrap
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        protected override Task SelfInitialization()
        {
            _sceneLoader.LoadTargetScene();
            return Task.CompletedTask;
        }
    }
}