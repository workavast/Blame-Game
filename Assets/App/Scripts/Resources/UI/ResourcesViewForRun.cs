using App.Resources.ForRun;
using App.Resources.Storage;
using Zenject;

namespace App.Resources.UI
{
    public class ResourcesViewForRun : ResourcesView
    {
        [Inject] private readonly ResourcesForRunProvider _resourcesForRunProvider;
        
        protected override IReadOnlyResourceStorage ResourceStorage => _resourcesForRunProvider.GetResourceStorage();
    }
}