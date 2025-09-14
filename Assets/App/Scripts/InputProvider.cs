using UnityEngine;

namespace App
{
    public class InputProvider : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayers;

        public InputSystem_Actions Input { get; private set; }
        public Vector3 LookPoint { get; private set; }
        
        private void Awake()
        {
            Input = new InputSystem_Actions();
            Input.Enable();
            
            ServiceLocator.Add(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove(this);
        }

        private void Update()
        {
            var mouseInput = Input.Player.MousePoint.ReadValue<Vector2>();
            var ray = Camera.main.ScreenPointToRay(mouseInput);

            if (Physics.Raycast(ray, out var hit, 50f, groundLayers)) 
                LookPoint = hit.point;
        }
    }
}