using App.Resources.Cells;
using TMPro;
using UnityEngine;
using Zenject;

namespace App.Resources.ForRun
{
    public class ScrapForRunView : MonoBehaviour
    {
        [SerializeField] private ResourceType resource;
        [SerializeField] private TMP_Text text;
        
        [Inject] private readonly ResourcesForRunProvider _resourcesForRunProvider;

        private IReadOnlyResourceCell _resourceCell;

        private void Awake()
        {
            _resourceCell = _resourcesForRunProvider.GetResourceCell(resource);
        }

        private void OnEnable()
        {
            _resourceCell.OnChanged += UpdateView;
            UpdateView(_resourceCell.Amount);
        }

        private void OnDisable() 
            => _resourceCell.OnChanged -= UpdateView;

        private void UpdateView(int scrapAmount)
        {
            text.text = scrapAmount.ToString();
        }
    }
}