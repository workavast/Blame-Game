using System;
using App.Bestiary.Article;
using App.Utils;
using UnityEngine;

namespace App.Bestiary
{
    public class BestiaryManager : MonoBehaviour
    {
        [SerializeField] private BestiaryConfig config;
        [SerializeField] private int defaultIndex;
        [SerializeField] private Canvas bestiaryUi;
        [SerializeField] private ArticleManager articleManager;
        
        public int ActiveArticle { get; private set; } = -1;

        private BestiaryHolder _bestiaryHolder;
        
        public event Action OnActiveArticleChanged;

        public void Initialize(BestiaryHolder bestiaryHolder)
        {
            _bestiaryHolder = bestiaryHolder;
            articleManager.Initialize(config.BestiaryArticles.Count, defaultIndex);
            
            SetArticle(defaultIndex);
        }
        
        public void ToggleVisibility(bool isVisible)
        {
            bestiaryUi.gameObject.SetActive(isVisible);
            if (isVisible) 
                articleManager.UpdateTexts();
        }

        public void Close() 
            => _bestiaryHolder.Close();

        public void NextModel() 
            => SetArticle(ActiveArticle + 1);

        public void PrevModel() 
            => SetArticle(ActiveArticle - 1);

        public void SetArticle(int index)
        {
            index = MathfExt.Repeat(index, config.BestiaryArticles.Count);
            
            if (ActiveArticle == index)
                return;

            if (!config.Has(index))
                return;

            ActiveArticle = index;
            var articleConfig = config.BestiaryArticles[ActiveArticle];
            articleManager.SetArticle(ActiveArticle, articleConfig);
            
            OnActiveArticleChanged?.Invoke();
        }
    }
}