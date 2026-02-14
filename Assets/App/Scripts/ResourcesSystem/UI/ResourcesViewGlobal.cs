using App.ResourcesSystem.Storage;
using Zenject;

namespace App.ResourcesSystem.UI
{
    public class ResourcesViewGlobal : ResourcesView
    {
        [Inject] private readonly IReadOnlyResourceStorage _resourceStorage;
        
        protected override IReadOnlyResourceStorage ResourceStorage => _resourceStorage;
    }
}