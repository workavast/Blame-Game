using System.Threading;
using System.Threading.Tasks;
using App.Bootstraps;
using Zenject;

namespace App.Resources.ForRun
{
    public class ResourcesForRunBootstrap : Bootstrap
    {
        [Inject] private readonly ResourcesForRunProvider _resourcesForRunProvider;
        
        protected override Task SelfInitialization(CancellationToken cancellationToken)
        {
            ServicesBridge.Add(_resourcesForRunProvider);
            return Task.CompletedTask;
        }

        protected override void OnDestroy()
        {
            ServicesBridge.Remove(_resourcesForRunProvider);
            base.OnDestroy();
        }
    }
}