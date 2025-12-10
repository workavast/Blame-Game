using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary.Article.UI
{
    public class ArticleView : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private Image view;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color unActiveColor;

        private int _index;
        public event Action<int> OnPressed;

        private void Awake()
        {
            btn.onClick.AddListener(() => OnPressed?.Invoke(_index));
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
        
        public void SetActivityState(bool isActive)
        {
            if (isActive)
                view.color = activeColor;
            else
                view.color = unActiveColor;
        }
    }
}