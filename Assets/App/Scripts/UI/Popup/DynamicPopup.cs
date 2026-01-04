using TMPro;
using UnityEngine;

namespace App.UI.Popup
{
    public class DynamicPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        public void SetData(string title, string description)
        {
            titleText.text = title;
            descriptionText.text = description;
        }
        
        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}
