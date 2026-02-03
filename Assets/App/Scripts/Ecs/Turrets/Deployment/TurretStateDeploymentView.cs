using System;
using App.Ecs.EntityViews;
using DG.Tweening;
using UnityEngine;

namespace App.Ecs.Turrets.Deployment
{
    public class TurretStateDeploymentView : MonoBehaviour, IEntityViewElement
    {
        [SerializeField] private Ease ease = Ease.Linear;
        [SerializeField] private Transform sphere;
        [SerializeField] private Light sphereLight;

        private float _initialLightIntensity;
        
        public event Action<IEntityViewElement> OnCleanupCompleted;

        private void Awake()
        {
            sphere.localScale = Vector3.zero;
            _initialLightIntensity = sphereLight.intensity;
            sphereLight.intensity = 0;
        }

        public bool OnDestroyCallback() 
            => true;
        
        public void SetDeployPercentageTime(float value)
        {
            value = Mathf.Clamp01(value);
            var easedValue = DOVirtual.EasedValue(0f, 1f, value, ease);
            var lightIntensity = _initialLightIntensity * easedValue;
            
            sphere.localScale = new Vector3(easedValue, easedValue, easedValue);
            sphereLight.intensity = lightIntensity;
        }
    }
}