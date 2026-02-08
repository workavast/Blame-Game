using App.Resources.Storage;
using Zenject;

namespace App.Resources.UI
{
    public class ResourcesViewGlobal : ResourcesView
    {
        [Inject] private readonly IReadOnlyResourceStorage _resourceStorage;
        
        protected override IReadOnlyResourceStorage ResourceStorage => _resourceStorage;
    }
}