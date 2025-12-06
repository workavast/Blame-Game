using UnityEngine;

namespace App
{
    public class CameraDisabler : MonoBehaviour
    {
        private void Awake()
        {
            if (!Application.isEditor)
                gameObject.SetActive(false);
        }
    }
}