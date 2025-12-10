using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Bestiary.CameraControl
{
    public class CameraScrollReader : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private InputActionReference scrollAction;

        private void Awake()
        {
            scrollAction.action.Enable();
        }

        public void Update()
        {
            var scrollDelta = -scrollAction.action.ReadValue<Vector2>().y;
            if (scrollDelta == 0)
                return;
            
            cameraManager.Scroll(scrollDelta);
        }
    }
}