using System.Collections.Generic;
using UnityEngine;

namespace App.Bestiary
{
    [CreateAssetMenu(fileName = nameof(BestiaryConfig), menuName = BestiaryConsts.BestiaryConfigsPath + nameof(BestiaryConfig))]
    public class BestiaryConfig : ScriptableObject
    {
        [SerializeField] private List<BestiaryArticleConfig> bestiaryArticles;

        public IReadOnlyList<BestiaryArticleConfig> BestiaryArticles => bestiaryArticles;

        public bool Has(int index) 
            => 0 <= index && index < BestiaryArticles.Count;
    }
}