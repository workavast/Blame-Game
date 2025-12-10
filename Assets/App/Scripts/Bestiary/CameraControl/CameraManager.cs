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
        [SerializeField] private Transform cameraDistanceTransform;
        [SerializeField] private float scrollPower;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float defaultDistance;

        private float _currentDistance;

        private void Awake()
        {
            _currentDistance = defaultDistance;
            Scroll(0);
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
            var distanceDelta = (scrollPower * scrollDelta * Time.deltaTime);
            _currentDistance = Mathf.Clamp(_currentDistance + distanceDelta, minDistance, maxDistance);

            cameraDistanceTransform.localPosition = new Vector3(0, 0, -_currentDistance);
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