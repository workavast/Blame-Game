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
        private BestiaryArticleConfig _activeConfig;

        public void Initialize(int articlesCount, int initialIndex)
        {
            articlesList.Initialize(articlesCount, initialIndex);
        }
        
        public void SetArticle(int index, BestiaryArticleConfig articleConfig)
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
        
        public void UpdateTexts()
        {
            articleDescription.SetTitle(_activeConfig.TitleName);
            articleDescription.SetDescription(_activeConfig.Description);
        }
    }
}