using System;

namespace App.ResourcesSystem.Saves
{
    [Serializable]
    public class ResourcesSaveModel
    {
        public ResourceCellSaveModel[] resources;
    }
    
    [Serializable]
    public class ResourceCellSaveModel
    {
        public ResourceType resourceId;
        public int amount;
    }
}