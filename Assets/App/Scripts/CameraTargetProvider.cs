using UnityEngine;

namespace App
{
    public class CameraTargetProvider : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        
        public Transform CameraTarget => cameraTarget;

        private void Awake()
        {
            ServicesBridge.Add(this);
        }

        private void OnDestroy()
        {
            ServicesBridge.Remove(this);
        }
    }
}