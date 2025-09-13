using UnityEngine;
using Object = UnityEngine.Object;

namespace App
{
    public class SpawnProvider : MonoBehaviour
    {
        private void Awake() 
            => ServiceLocator.Add(this);

        private void OnDestroy() 
            => ServiceLocator.Remove(this);

        public T Spawn<T>(T prefab) where T : Object 
            => Instantiate(prefab, transform);
    }
}