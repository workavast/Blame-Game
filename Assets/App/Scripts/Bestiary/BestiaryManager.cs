using System;
using App.Bestiary.Article;
using App.Bestiary.CameraControl;
using App.EscProviding;
using UnityEngine;

namespace App.Bestiary
{
    public class BestiaryManager : MonoBehaviour
    {
        [SerializeField] private int defaultArticleIndex;
        [SerializeField] private ArticlesManager articleManager;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private BestiaryCloseReader bestiaryCloseReader;
        
        public event Action OnCloseRequested;

        public void Initialize(EscProvider escProvider)
        {
            articleManager.Initialize();
            bestiaryCloseReader.Initialize(escProvider);
            bestiaryCloseReader.OnCloseRequested += RequestClose;
        }

        public void ToggleVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
            if (isVisible)
            {
                cameraManager.ToDefault();
                articleManager.ToDefault(defaultArticleIndex);
            }
        }
        
        private void RequestClose() 
            => OnCloseRequested?.Invoke();
    }
}