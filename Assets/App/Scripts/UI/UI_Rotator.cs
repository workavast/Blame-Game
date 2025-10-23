using Unity.Mathematics;
using UnityEngine;

namespace App.UI
{
    public class UI_Rotator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;

        private void Update()
        {
            var currentRotation = transform.rotation.eulerAngles.z;
            var newRotation = Quaternion.Euler(0, 0, currentRotation + rotateSpeed * Time.unscaledDeltaTime);
            transform.rotation = newRotation;
        }
    }
}