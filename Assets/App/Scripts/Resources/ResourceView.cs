using App.Resources.Cells;
using App.Resources.Storage;
using TMPro;
using UnityEngine;
using Zenject;

namespace App.Resources
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private ResourceType resource;
        [SerializeField] private TMP_Text text;
        
        [Inject] private readonly IReadOnlyResourceStorage _resourcesStorage;
        
        private IReadOnlyResourceCell _resourceCell;

        private void Awake()
        {
            _resourceCell = _resourcesStorage.GetResourceCell(resource);
        }

        private void OnEnable()
        {
            _resourceCell.OnChanged += UpdateValue;
            UpdateValue(_resourceCell.Amount);
        }

        private void OnDisable()
        {
            _resourceCell.OnChanged -= UpdateValue;
        }

        private void UpdateValue(int amount) 
            => text.text = amount.ToString();
    }
}