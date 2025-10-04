using Unity.Entities.Content;
using UnityEngine;

namespace App.Views
{
    public class CleanupView : MonoBehaviour
    {
        [SerializeField] private CleanupCallback cleanupCallback;

        protected WeakObjectReference<CleanupView> _prefab;

        public CleanupCallback CleanupCallback => cleanupCallback;
        
        protected virtual void Awake()
        {
            cleanupCallback.SetCallback(DestroyCallback);
        }
        
        protected virtual void DestroyCallback()
        {
            _prefab.Release();
            Destroy(gameObject);
        }

        public void SetPrefab(ref WeakObjectReference<CleanupView> prefab) 
            => _prefab = prefab;
    }
}