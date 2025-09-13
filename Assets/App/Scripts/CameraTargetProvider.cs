using UnityEngine;

namespace App
{
    public class CameraTargetProvider : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        
        public Transform CameraTarget => cameraTarget;

        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove(this);
        }
    }
}