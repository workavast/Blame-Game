using System.Collections.Generic;
using UnityEngine;

namespace App.Bestiary.Article
{
    [CreateAssetMenu(fileName = nameof(ArticleConfigsRep), menuName = BestiaryConsts.BestiaryConfigsPath + nameof(ArticleConfigsRep))]
    public class ArticleConfigsRep : ScriptableObject
    {
        [SerializeField] private List<ArticleConfig> bestiaryArticles;

        public IReadOnlyList<ArticleConfig> BestiaryArticles => bestiaryArticles;

        public bool Has(int index) 
            => 0 <= index && index < BestiaryArticles.Count;
    }
}