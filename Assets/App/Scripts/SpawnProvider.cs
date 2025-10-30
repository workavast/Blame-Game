using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace App
{
    public class SpawnProvider : MonoBehaviour
    {
        [Inject] private readonly DiContainer _diContainer;
        
        private void Awake() 
            => ServicesBridge.Add(this);

        private void OnDestroy() 
            => ServicesBridge.Remove(this);

        public T Spawn<T>(T prefab) where T : Object
        {
            var instance = _diContainer.InstantiatePrefab(prefab, transform);
            return instance.GetComponent<T>();
        }
    }
}