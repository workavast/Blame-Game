using UnityEngine;
using UnityEngine.Localization;

namespace App.Bestiary
{
    [CreateAssetMenu(fileName = nameof(BestiaryArticleConfig), menuName = BestiaryConsts.BestiaryConfigsPath + nameof(BestiaryArticleConfig))]
    public class BestiaryArticleConfig : ScriptableObject
    {
        [SerializeField] private GameObject model;
        [SerializeField] private LocalizedString titleName;
        [SerializeField] private LocalizedString description;

        public GameObject Model => model;
        public string TitleName => titleName.GetLocalizedString();
        public string Description => description.GetLocalizedString();
    }
}