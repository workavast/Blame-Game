using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Clenuping
{
    public class CleanupView : MonoBehaviour
    {
        [SerializeField] private App.CleanupCallback cleanupCallback;

        protected WeakObjectReference<CleanupView> _prefab;

        public App.CleanupCallback CleanupCallback => cleanupCallback;
        
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