using App.ResourcesSystem.Storage;
using App.Saves;
using UnityEngine;

namespace App.ResourcesSystem.Saves
{
    public class ResourcesSaveModule : SaveModule<ResourcesSaveModel>
    {
        public ResourcesSaveModule(string filePath) : base(filePath)
        {
            
        }

        public void Save(ResourcesStorage resourcesStorage)
        {
            var resources = resourcesStorage.GetAmounts();
            var saveModel = new ResourcesSaveModel
            {
                resources = new ResourceCellSaveModel[resources.Count]
            };

            var i = 0;
            foreach (var resource in resources)
            {
                saveModel.resources[i] = new ResourceCellSaveModel
                {
                    resourceId = resource.Key,
                    amount = resource.Value
                };
                i++;
            }
            
            Save(saveModel);
        }
        
        public void Load(ResourcesStorage resourcesStorage)
        {
            var saveModel = Load();

            if (saveModel?.resources == null)
                return;
            
            foreach (var resource in saveModel.resources) 
                resourcesStorage.Add(resource.resourceId, resource.amount);
        }
    }
}