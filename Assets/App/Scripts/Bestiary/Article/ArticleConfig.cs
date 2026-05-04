using UnityEngine;
using UnityEngine.Localization;

namespace App.Bestiary.Article
{
    [CreateAssetMenu(fileName = nameof(ArticleConfig), menuName = BestiaryConsts.BestiaryConfigsPath + nameof(ArticleConfig))]
    public class ArticleConfig : ScriptableObject
    {
        [SerializeField] private GameObject model;
        [SerializeField] private LocalizedString titleName;
        [SerializeField] private LocalizedString description;

        public GameObject Model => model;
        public LocalizedString Title => titleName;
        public LocalizedString Description => description;
    }
}