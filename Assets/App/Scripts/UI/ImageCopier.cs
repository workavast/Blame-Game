using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class ImageCopier : MonoBehaviour
    {
        [SerializeField] private Image sourceImage;
        [SerializeField] private Image thisImage;

        private void Start()
        {
            thisImage.sprite = sourceImage.sprite;
        }
    }
}