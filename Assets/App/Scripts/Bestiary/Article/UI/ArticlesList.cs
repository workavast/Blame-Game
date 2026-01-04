using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Bestiary.Article.UI
{
    public class ArticlesList : MonoBehaviour
    {
        [SerializeField] private ArticleView articlePrefab;
        [SerializeField] private RectTransform viewsHolder;

        private ArticleView _lastActiveArticle;
        private readonly List<ArticleView> _articleViews = new();

        public event Action<int> OnManualActivateArticle;

        public void Initialize(int articlesCount, int initialIndex)
        {
            for (int i = 0; i < viewsHolder.childCount; i++) 
                Destroy(viewsHolder.GetChild(i).gameObject);
            
            _articleViews.Capacity = articlesCount;
            for (int i = 0; i < articlesCount; i++)
            {
                var article = Instantiate(articlePrefab, viewsHolder);
                article.SetIndex(i);
                article.SetActivityState(false);
                article.OnPressed += ManualSetArticle;
                _articleViews.Add(article);
            }

            ActivateArticle(initialIndex);
        }
        
        public void ActivateArticle(int articleIndex)
        {
            if (_lastActiveArticle != null)
                _lastActiveArticle.SetActivityState(false);
            
            var article = _articleViews[articleIndex];
            article.SetActivityState(true);
            
            _lastActiveArticle = article;
        }

        private void ManualSetArticle(int index) 
            => OnManualActivateArticle?.Invoke(index);
    }
}