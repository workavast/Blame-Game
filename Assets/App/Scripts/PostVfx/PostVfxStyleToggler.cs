using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace App.PostVfx
{
    public class PostVfxStyleToggler : MonoBehaviour
    {
        [SerializeField] private UniversalRendererData rendererData;
        [SerializeField] private string featureName1 = "Outline1";
        [Space]
        [SerializeField] private List<Material> postProcMaterials;
        [SerializeField] private RawImage rawImage;
        
        private readonly Dictionary<string, ScriptableRendererFeature> _renderFeatures = new();
        private bool _effIsActive = false;
        
        private void Start()
        {
            foreach (var feature in rendererData.rendererFeatures) 
                _renderFeatures[feature.name] = feature;
        }
        
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
        
        public void ToggleEff(InputAction.CallbackContext input)
        {
            if (!input.started)
                return;

            _effIsActive = !_effIsActive;
            if (_renderFeatures.TryGetValue(featureName1, out var renderFeature1)) 
                renderFeature1.SetActive(_effIsActive);
        }
    }
}