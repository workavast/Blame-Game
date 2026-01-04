using App.CameraShaking;
using UnityEngine;
using Zenject;

namespace App.PlayerProviding
{
    public class CameraShakeOnPlayerDamage : MonoBehaviour
    {
        [SerializeField] private float shakePower;
        
        [Inject] private CameraShakeBehaviour _cameraShakeBehaviour;
        [Inject] private PlayerProvider _playerProvider;

        private float _lastHealthFillPercentage;
        
        private void Update()
        {
            if (_playerProvider.FillPercentage < _lastHealthFillPercentage) 
                _cameraShakeBehaviour.Shake(shakePower);
            
            _lastHealthFillPercentage = _playerProvider.FillPercentage;
        }
    }
}