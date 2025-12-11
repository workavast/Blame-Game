using System;
using UnityEngine;

namespace App.Bestiary.CameraControl
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform cameraRotationHolder;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minAngle;
        [Space]
        [SerializeField] private Scroller scroller;

        private void Awake()
        {
            scroller.Initialize();
            Scroll(0);
        }

        private void Update()
        {
            scroller.Update();
        }

        public void Rotate(Vector2 delta)
        {
            cameraRotationHolder.Rotate(Vector3.up, delta.x * rotationSpeed, Space.World);

            var angle = NormalizeAngle(cameraRotationHolder.rotation.eulerAngles.x);
            var angleDelta = -delta.y * rotationSpeed;
            var clampedAngle = Mathf.Clamp(angleDelta, minAngle - angle, maxAngle - angle);

            cameraRotationHolder.Rotate(Vector3.right, clampedAngle, Space.Self);
        }

        public void Scroll(float scrollDelta)
        {
            scroller.SetTargetDistance(scrollDelta);
        }

        private static float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f) 
                angle -= 360f;
            return angle;
        }

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
                _currentDistance = _targetDistance = defaultDistance;
                cameraDistanceTransform.localPosition = new Vector3(0, 0, -_currentDistance);              
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
            
            public void SetTargetDistance(float scrollDelta)
            {
                var distanceDelta = (scrollReadPower * scrollDelta);
                _targetDistance = Mathf.Clamp(_targetDistance + distanceDelta, minDistance, maxDistance);
            }
        }
    }
}