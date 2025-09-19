using UnityEngine;

namespace App.Views
{
    public class CleanupView : MonoBehaviour
    {
        [SerializeField] private CleanupCallback cleanupCallback;

        public CleanupCallback CleanupCallback => cleanupCallback;
        
        protected virtual void Awake()
        {
            cleanupCallback.SetCallback(DestroyCallback);
        }
        
        protected virtual void DestroyCallback()
        {
            Debug.Log($"{name} is destroyed");
        }
    }
}