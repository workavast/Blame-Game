using System;
using UnityEngine;

namespace App.Bestiary.CameraControl
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Rotator rotator;
        [SerializeField] private Scroller scroller;

        private void Awake()
        {
            scroller.Initialize();
            rotator.Initialize();
        }

        private void Update()
        {
            rotator.Update();
            scroller.Update();
        }

        public void ToDefault()
        {
            scroller.ToDefault();
            rotator.ToDefault();
        }
        
        public void Rotate(Vector2 delta) 
            => rotator.Rotate(delta);

        public void Scroll(float scrollDelta) 
            => scroller.SetTargetDistance(scrollDelta);

        [Serializable]
        private class Scroller
        {
            [SerializeField] private Transform cameraDistanceTransform;
            [SerializeField] private float scrollReadPower;
            [SerializeField] private float damping;
            [SerializeField] private float minDistance;
            [SerializeField] private float maxDistance;
            [SerializeField] private float defaultDistance;

            private float _targetDistance;
            private float _currentDistance;

            public void Initialize()
            {
                ToDefault();
            }
            
            public void Update()
            {
                if (Mathf.Approximately(_currentDistance, _targetDistance)) 
                    return;
                
                var t = 1f - Mathf.Exp(-damping * Time.deltaTime);
                _currentDistance = Mathf.Lerp(_currentDistance, _targetDistance, t);
                if (Mathf.Approximately(_currentDistance, _targetDistance)) 
                    _currentDistance = _targetDistance;
                
                cameraDistanceTransform.localPosition = new Vector3(0, 0, -_currentDistance);              
            }
            
            public void ToDefault()
            {
                _currentDistance = _targetDistance = Mathf.Clamp(defaultDistance, minDistance, maxDistance);
                cameraDistanceTransform.localPosition = new Vector3(0, 0, -_currentDistance);
            }
            
            public void SetTargetDistance(float scrollDelta)
            {
                var distanceDelta = (scrollReadPower * scrollDelta);
                _targetDistance = Mathf.Clamp(_targetDistance + distanceDelta, minDistance, maxDistance);
            }
        }

        [Serializable]
        private class Rotator
        {
            [SerializeField] private Transform cameraRotationHolder;
            [SerializeField] private float rotationSpeed;
            [SerializeField] private float maxAngle;
            [SerializeField] private float minAngle;
            [SerializeField] private float damping;

            private float _currentYaw;
            private float _targetYaw;

            private float _currentPitch;
            private float _targetPitch;

            private Vector3 _defaultRotation;
            
            public void Initialize()
            {
                _defaultRotation = cameraRotationHolder.rotation.eulerAngles;
                _currentYaw = _targetYaw = _defaultRotation.y;
                _currentPitch = _targetPitch = NormalizeAngle(_defaultRotation.x);
            }
            
            public void Update()
            {
                var t = 1f - Mathf.Exp(-damping * Time.deltaTime);

                _currentYaw = Mathf.Lerp(_currentYaw, _targetYaw, t);
                _currentPitch = Mathf.Lerp(_currentPitch, _targetPitch, t);

                cameraRotationHolder.rotation = Quaternion.Euler(_currentPitch, _currentYaw, 0f);
            }

            public void ToDefault()
            {
                _currentYaw = _targetYaw = _defaultRotation.y;
                _currentPitch = _targetPitch = NormalizeAngle(_defaultRotation.x);
            }
            
            public void Rotate(Vector2 delta)
            {
                _targetYaw += delta.x * rotationSpeed;
                _targetPitch -= delta.y * rotationSpeed;
                _targetPitch = Mathf.Clamp(_targetPitch, minAngle, maxAngle);
            }

            private static float NormalizeAngle(float angle)
            {
                angle %= 360f;
                if (angle > 180f) 
                    angle -= 360f;
                return angle;
            }
        }
    }
}