using App.Bestiary.Article.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace App.Bestiary.Article
{
    public class ArticlesInputReader : MonoBehaviour
    {
        [SerializeField] private Button nextArticleBtn;
        [SerializeField] private Button prevArticleBtn;
        [SerializeField] private InputActionReference nextBtn;
        [SerializeField] private InputActionReference prevBtn;
        [SerializeField] private ArticlesList articlesList;
        [SerializeField] private ArticlesManager articlesManager;
        
        private void OnEnable()
        {
            nextBtn.action.Enable();
            prevBtn.action.Enable();
        }
        
        private void Start()
        {
            nextBtn.action.performed += NextModel;
            prevBtn.action.performed += PrevModel;
            nextArticleBtn.onClick.AddListener(articlesManager.NextModel);
            prevArticleBtn.onClick.AddListener(articlesManager.PrevModel);
            articlesList.OnManualActivateArticle += articlesManager.SetArticle;
        }

        private void OnDisable()
        {
            nextBtn.action.Disable();
            prevBtn.action.Disable();
        }

        private void OnDestroy()
        {
            nextBtn.action.performed -= NextModel;
            prevBtn.action.performed -= PrevModel;
            nextArticleBtn.onClick.RemoveListener(articlesManager.NextModel);
            prevArticleBtn.onClick.RemoveListener(articlesManager.PrevModel);
            articlesList.OnManualActivateArticle -= articlesManager.SetArticle;
        }

        private void NextModel(InputAction.CallbackContext obj) 
            => articlesManager.NextModel();
        
        private void PrevModel(InputAction.CallbackContext obj) 
            => articlesManager.PrevModel();
    }
}