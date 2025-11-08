using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace App.PostVfx
{
    public class PostVfxStyleToggler : MonoBehaviour
    {
        [SerializeField] private List<Material> postProcMaterials;
        [SerializeField] private RawImage rawImage;
        
        public void InvNums(InputAction.CallbackContext input)
        {
            if (!input.started)
                return;
            
            var floatValue = input.ReadValue<float>();

            var index = ((int)floatValue) - 1;
            if (index < 0 || postProcMaterials.Count <= index) 
                rawImage.material = null;
            else
                rawImage.material = postProcMaterials[index];
        }
    }
}