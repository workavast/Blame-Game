using System;
using App.Bestiary.Article;
using App.Bestiary.CameraControl;
using App.EscProviding;
using App.Utils;
using UnityEngine;

namespace App.Bestiary
{
    public class BestiaryManager : MonoBehaviour, IEscListener
    {
        [SerializeField] private BestiaryConfig config;
        [SerializeField] private int defaultIndex;
        [SerializeField] private ArticleManager articleManager;
        [SerializeField] private CameraManager cameraManager;
        
        public int ActiveArticle { get; private set; } = -1;

        private BestiaryHolder _bestiaryHolder;
        private EscProvider _escProvider;
        
        public event Action OnActiveArticleChanged;

        public void Initialize(BestiaryHolder bestiaryHolder, EscProvider escProvider)
        {
            _bestiaryHolder = bestiaryHolder;
            _escProvider = escProvider;
            
            articleManager.Initialize(config.BestiaryArticles.Count, defaultIndex);
        }

        private void OnDestroy()
        {
            _escProvider.UnSub(this);
        }

        public void ToggleVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
            if (isVisible)
            {
                cameraManager.ToDefault();
                SetArticle(defaultIndex);
                _escProvider.Sub(this);
            }
            else
            {
                _escProvider.UnSub(this);
            }
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

        public void OnEscPressed() 
            => Close();
    }
}