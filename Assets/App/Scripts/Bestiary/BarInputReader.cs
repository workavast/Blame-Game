using App.Bestiary.Article.UI;
using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary
{
    public class BarInputReader : MonoBehaviour
    {
        [SerializeField] private Button nextBtn;
        [SerializeField] private Button prevBtn;
        [SerializeField] private ArticlesList articlesList;
        [SerializeField] private BestiaryManager bestiaryManager;

        private void Start()
        {
            nextBtn.onClick.AddListener(MoveToNext);
            prevBtn.onClick.AddListener(MoveToPrev);
            articlesList.OnManualActivateArticle += SetArticle;
        }

        private void OnDestroy()
        {
            nextBtn.onClick.RemoveListener(MoveToNext);
            prevBtn.onClick.RemoveListener(MoveToPrev);
        }

        private void SetArticle(int index) 
            => bestiaryManager.SetArticle(index);

        private void MoveToNext() 
            => bestiaryManager.NextModel();
        
        private void MoveToPrev()
            => bestiaryManager.PrevModel();
    }
}