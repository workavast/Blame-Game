using System;
using App.Utils;
using UnityEngine;

namespace App.Bestiary.Article
{
    public class ArticlesManager : MonoBehaviour
    {
        [SerializeField] private ArticleManager articleManager;
        [SerializeField] private ArticleConfigsRep config;
        
        public int ActiveArticle { get; private set; } = -1;
        
        public event Action OnActiveArticleChanged;

        public void Initialize()
        {
            articleManager.Initialize(config.BestiaryArticles.Count);
        }

        public void ToDefault(int defaultIndex)
        {
            SetArticle(defaultIndex);
            articleManager.RestDescription();
        }

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