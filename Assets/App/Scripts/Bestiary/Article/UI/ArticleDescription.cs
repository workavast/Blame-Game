using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary.Article.UI
{
    public class ArticleDescription : MonoBehaviour
    {
        [SerializeField] private Button visibilityToggleBtn;
        [SerializeField] private GameObject descriptionHolder;
        [SerializeField] private TMP_Text descriptionField;
        [SerializeField] private TMP_Text titleField;

        private void Awake()
        {
            visibilityToggleBtn.onClick.AddListener(ToggleVisibility);
        }

        private void ToggleVisibility()
        {
            var isVisible = !descriptionHolder.activeSelf;
            descriptionHolder.SetActive(isVisible);
        }

        public void SetTitle(string title)
        {
            titleField.text = title;
        }
        
        public void SetDescription(string description)
        {
            descriptionField.text = description;
        }
    }
}