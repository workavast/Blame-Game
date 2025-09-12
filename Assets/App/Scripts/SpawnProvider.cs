using UnityEngine;
using Object = UnityEngine.Object;

namespace App
{
    public class SpawnProvider : MonoBehaviour
    {
        public static SpawnProvider Instance;
        
        private void Awake()
        {
            Instance = this;
        }

        public static T Spawn<T>(T prefab) where T : Object 
            => Instantiate(prefab, Instance.transform);
    }
}