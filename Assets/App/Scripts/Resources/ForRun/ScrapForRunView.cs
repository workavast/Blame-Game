using TMPro;
using UnityEngine;
using Zenject;

namespace App.Resources.ForRun
{
    public class ScrapForRunView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        [Inject] private readonly ResourcesForRunProvider _resourcesForRunProvider;

        private void OnEnable()
        {
            _resourcesForRunProvider.OnScrapChanged += UpdateView;
            UpdateView(_resourcesForRunProvider.Scrap);
        }

        private void OnDisable() 
            => _resourcesForRunProvider.OnScrapChanged -= UpdateView;

        private void UpdateView(int scrapAmount)
        {
            text.text = scrapAmount.ToString();
        }
    }
}