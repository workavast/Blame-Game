using App.Bestiary.Article.UI;
using UnityEngine;

namespace App.Bestiary.Article
{
    public class ArticleManager : MonoBehaviour
    {
        [SerializeField] private Transform modelHolder;
        [SerializeField] private ArticlesList articlesList;
        [SerializeField] private ArticleDescription articleDescription;
        [SerializeField] private bool descriptionIsVisibleByDefault = true;

        private GameObject _activeModel;

        public void Initialize(int articlesCount)
        {
            articlesList.Initialize(articlesCount);
        }

        public void SetArticle(int index, ArticleConfig articleConfig)
        {
            LoadModel(articleConfig);
            UpdateTexts(articleConfig);
            articlesList.ActivateArticle(index);
        }

        public void RestDescription() 
            => articleDescription.SetVisibility(descriptionIsVisibleByDefault);

        private void LoadModel(ArticleConfig articleConfig)
        {
            if (_activeModel != null) 
                Destroy(_activeModel);

            _activeModel = Instantiate(articleConfig.Model, modelHolder);
        }
        
        private void UpdateTexts(ArticleConfig articleConfig)
        {
            articleDescription.SetTitle(articleConfig.Title);
            articleDescription.SetDescription(articleConfig.Description);
        }
    }
}