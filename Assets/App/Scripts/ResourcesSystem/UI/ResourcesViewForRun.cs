using App.ResourcesSystem.ForRun;
using App.ResourcesSystem.Storage;
using Zenject;

namespace App.ResourcesSystem.UI
{
    public class ResourcesViewForRun : ResourcesView
    {
        [Inject] private readonly ResourcesForRunProvider _resourcesForRunProvider;
        
        protected override IReadOnlyResourceStorage ResourceStorage => _resourcesForRunProvider.GetResourceStorage();
    }
}