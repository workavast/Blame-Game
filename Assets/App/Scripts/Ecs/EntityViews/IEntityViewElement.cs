using System;

namespace App.Ecs.EntityViews
{
    public interface IEntityViewElement
    {
        public event Action<IEntityViewElement> OnCleanupCompleted;
        
        public bool OnDestroyCallback();
    }
}