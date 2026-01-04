using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace App.PostVfx
{
    public class PostVfxStyleToggler : MonoBehaviour
    {
        [SerializeField] private InputActionReference numInput;
        [SerializeField] private List<Material> postProcMaterials;
        [SerializeField] private RawImage rawImage;

        private void Awake()
        {
            numInput.action.performed += ReadInput;
        }

        private void OnDestroy()
        {
            numInput.action.performed -= ReadInput;
        }

        private void ReadInput(InputAction.CallbackContext input)
        {
            var floatValue = input.ReadValue<float>();
            var index = ((int)floatValue) - 1;
            
            SetVfx(index);
        }

        private void SetVfx(int index)
        {
            if (index < 0 || postProcMaterials.Count <= index) 
                rawImage.material = null;
            else
                rawImage.material = postProcMaterials[index];
        }
    }
}