using App.ResourcesSystem.Storage;
using App.Saves;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveModule : SaveModule<ResourcesStorageState>
    {
        public ResourcesSaveModule(string filePath) : base(filePath)
        {
            
        }

        public void Save(ResourcesStorage resourcesStorage)
        {
            var resources = resourcesStorage.GetAmounts();
            var saveState = new ResourcesStorageState
            {
                resources = new ResourceCellState[resources.Count]
            };

            var i = 0;
            foreach (var resource in resources)
            {
                saveState.resources[i] = new ResourceCellState
                {
                    resourceId = resource.Key,
                    amount = resource.Value
                };
                i++;
            }
            
            Save(saveState);
        }
        
        public void Load(ResourcesStorage resourcesStorage)
        {
            var saveState = Load();
            if (saveState?.resources == null)
                return;
            
            foreach (var resource in saveState.resources) 
                resourcesStorage.Add(resource.resourceId, resource.amount);
        }
    }
}