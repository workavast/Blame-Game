using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace App.Bestiary.Article.UI
{
    public class ArticleDescription : MonoBehaviour
    {
        [SerializeField] private Button visibilityToggleBtn;
        [SerializeField] private GameObject descriptionHolder;
        [SerializeField] private LocalizeStringEvent titleField;
        [SerializeField] private LocalizeStringEvent descriptionField;
        
        private void Awake()
        {
            visibilityToggleBtn.onClick.AddListener(ToggleVisibility);
        }

        public void SetVisibility(bool isVisible) 
            => descriptionHolder.SetActive(isVisible);

        public void SetTitle(LocalizedString title) 
            => titleField.StringReference = title;

        public void SetDescription(LocalizedString description) 
            => descriptionField.StringReference = description;
        
        private void ToggleVisibility() 
            => SetVisibility(!descriptionHolder.activeSelf);
    }
}