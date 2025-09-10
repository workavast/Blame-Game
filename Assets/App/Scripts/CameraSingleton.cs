using UnityEngine;

namespace App.Scripts
{
    public class CameraSingleton : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        
        public static CameraSingleton Instance;
        public Transform CameraTarget => cameraTarget;

        private void Awake()
        {
            Instance = this;
        }
    }
}