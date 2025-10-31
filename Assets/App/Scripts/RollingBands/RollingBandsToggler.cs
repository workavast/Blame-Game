using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.RollingBands
{
    public class RollingBandsToggler
    {
        private readonly RollingBandsVisibilityChanger _rollingBandsVisibilityChanger;
        private readonly ScriptableRendererFeature _rendererFeature;

        private int _visibilityRequestCount;

        public RollingBandsToggler(RollingBandsVisibilityChanger rollingBandsVisibilityChanger, UniversalRendererData rendererData, string linesEffectName)
        {
            _rollingBandsVisibilityChanger = rollingBandsVisibilityChanger;
            
            foreach (var feature in rendererData.rendererFeatures)
                if (feature.name == linesEffectName) 
                    _rendererFeature = feature;

            if (_rendererFeature == null) 
                Debug.LogError($"Cant find render feature with this name: [{linesEffectName}]");
            
            _rollingBandsVisibilityChanger.Toggle(false);
        }

        public void SetVisibilityState(bool isVisible)
        {
            var prevValue = _visibilityRequestCount;
            if (isVisible)
                _visibilityRequestCount++;
            else
                _visibilityRequestCount--;

            if (_visibilityRequestCount >= 1 && prevValue > 1)
                return;

            if (_visibilityRequestCount < 0)
            {
                _visibilityRequestCount = 0;
                Debug.LogWarning("You try hide lines effect when it already hided");
                return;
            }
            
            _rollingBandsVisibilityChanger.Toggle(_visibilityRequestCount > 0);
        }
    }
}