using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.RollingBands
{
    public class RollingBandsToggler
    {
        private readonly RollingBandsVisibilityChanger _rollingBandsVisibilityChanger;
        private readonly ScriptableRendererFeature _rendererFeature;
        private readonly RequestCounter _requestCounter;
        
        public RollingBandsToggler(RollingBandsVisibilityChanger rollingBandsVisibilityChanger, UniversalRendererData rendererData, string linesEffectName)
        {
            _rollingBandsVisibilityChanger = rollingBandsVisibilityChanger;
            
            foreach (var feature in rendererData.rendererFeatures)
                if (feature.name == linesEffectName) 
                    _rendererFeature = feature;

            if (_rendererFeature == null) 
                Debug.LogError($"Cant find render feature with this name: [{linesEffectName}]");
            
            _requestCounter = new RequestCounter(ApplyVisibilityState);
            ApplyVisibilityState(true);
        }

        public void SetVisibilityState(bool isVisible) 
            => _requestCounter.ChangeRequests(isVisible);

        private void ApplyVisibilityState(bool isVisible) 
            => _rollingBandsVisibilityChanger.Toggle(isVisible);
    }
}