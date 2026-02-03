using UnityEngine;

namespace App.Ecs.Turrets
{
    public class TurretSphereView : MonoBehaviour
    {
        [SerializeField] private Transform sphere;
        [SerializeField] private Light sphereLight;

        private float _initialLightIntensity;
        
        private void Awake()
        {
            sphere.localScale = Vector3.zero;
            _initialLightIntensity = sphereLight.intensity;
            sphereLight.intensity = 0;
        }

        public void SetScale(float scale)
        {
            var lightIntensity = _initialLightIntensity * scale;
            
            sphere.localScale = new Vector3(scale, scale, scale);
            sphereLight.intensity = lightIntensity;
        }
    }
}