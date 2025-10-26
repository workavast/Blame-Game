using UnityEngine;

namespace App
{
    public class PlayerInputProvider : MonoBehaviour
    {
        [SerializeField] private float rayDistance = 100f;
        [SerializeField] private LayerMask groundLayers;

        public InputSystem_Actions Input { get; private set; }
        public Vector3 LookPoint { get; private set; }
        
        private void Awake()
        {
            Input = new InputSystem_Actions();
            Input.Enable();
            
            ServicesBridge.Add(this);
        }

        private void OnDestroy()
        {
            Input.Disable();
            Input.Dispose();
            
            ServicesBridge.Remove(this);
        }

        private void Update()
        {
            var mouseInput = Input.Player.MousePoint.ReadValue<Vector2>();
            var ray = Camera.main.ScreenPointToRay(mouseInput);

            if (Physics.Raycast(ray, out var hit, rayDistance, groundLayers)) 
                LookPoint = hit.point;
        }
    }
}