using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Bestiary.CameraControl
{
    public class UICameraRotator : MonoBehaviour, IDragHandler
    {
        [SerializeField] private CameraManager cameraManager;

        public void OnDrag(PointerEventData eventData) 
            => cameraManager.Rotate(eventData.delta);
    }
}