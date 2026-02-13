using System;

namespace App.ResourcesSystem.Saves
{
    [Serializable]
    public class ResourcesStorageState
    {
        public ResourceCellState[] resources;
    }
    
    [Serializable]
    public class ResourceCellState
    {
        public ResourceType resourceId;
        public int amount;
    }
}