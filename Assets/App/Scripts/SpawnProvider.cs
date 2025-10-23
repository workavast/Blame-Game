using UnityEngine;
using Object = UnityEngine.Object;

namespace App
{
    public class SpawnProvider : MonoBehaviour
    {
        private void Awake() 
            => ServicesBridge.Add(this);

        private void OnDestroy() 
            => ServicesBridge.Remove(this);

        public T Spawn<T>(T prefab) where T : Object 
            => Instantiate(prefab, transform);
    }
}