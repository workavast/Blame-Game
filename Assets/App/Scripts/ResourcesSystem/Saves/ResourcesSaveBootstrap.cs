using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using Zenject;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveBootstrap : Bootstrap
    {
        [Inject] private readonly ResourcesSaveManager _resourcesSaveManager;
        
        protected override Task SelfInitialization(CancellationToken cancellationToken)
        {
            _resourcesSaveManager.Load();
            _resourcesSaveManager.Activate();
            return Task.CompletedTask;
        }
    }
}