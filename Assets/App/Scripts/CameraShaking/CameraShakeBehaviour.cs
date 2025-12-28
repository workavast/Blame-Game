using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace App.CameraShaking
{
    public class CameraShakeBehaviour : MonoBehaviour
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;
        [SerializeField] private NoiseConfig noiseConfig;
        [SerializeField] private float tickRateScale = 1;

        [Inject] private readonly CameraShakeSettingProvider _shakeSettingProvider;

        private float _maxTime = 1;
        private float _currentTime = 0;

        private void Awake()
        {
            _maxTime = noiseConfig.TimeLenght;
            perlin.NoiseProfile = noiseConfig.NoiseSettings;
            perlin.AmplitudeGain = 0;
        }

        private void Update()
        {
            if (_currentTime <= 0)
                return;

            var deltaTime = Time.deltaTime * tickRateScale;
            _currentTime = Mathf.Clamp(_currentTime - deltaTime, 0, _maxTime);
            perlin.AmplitudeGain = _shakeSettingProvider.ShakePower * (_currentTime / _maxTime);
        }
        
        public void Shake(float timePower)
        {
            _currentTime = Mathf.Clamp(_currentTime + timePower, 0, _maxTime);
        }
    }
}