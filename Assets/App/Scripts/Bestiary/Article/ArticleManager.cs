using App.Bestiary.Article.UI;
using UnityEngine;

namespace App.Bestiary.Article
{
    public class ArticleManager : MonoBehaviour
    {
        [SerializeField] private Transform modelHolder;
        [SerializeField] private ArticlesList articlesList;
        [SerializeField] private ArticleDescription articleDescription;

        private GameObject _activeModel;
        private ArticleConfig _activeConfig;

        public void Initialize(int articlesCount)
        {
            articlesList.Initialize(articlesCount);
        }

        public void SetArticle(int index, ArticleConfig articleConfig)
        {
            _activeConfig = articleConfig;
            
            LoadModel();
            UpdateTexts();
            articlesList.ActivateArticle(index);
        }

        private void LoadModel()
        {
            if (_activeModel != null) 
                Destroy(_activeModel);

            _activeModel = Instantiate(_activeConfig.Model, modelHolder);
        }
        
        private void UpdateTexts()
        {
            articleDescription.SetTitle(_activeConfig.Title);
            articleDescription.SetDescription(_activeConfig.Description);
        }
    }
}