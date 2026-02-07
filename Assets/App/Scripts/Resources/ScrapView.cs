using TMPro;
using UnityEngine;
using Zenject;

namespace App.Resources
{
    public class ScrapView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        [Inject] private readonly IReadOnlyResourceStorage _resourcesStorage;

        private void OnEnable()
        {
            _resourcesStorage.OnMoneyChanged += UpdateValue;
            UpdateValue(_resourcesStorage.Scrap);
        }

        private void OnDisable()
        {
            _resourcesStorage.OnMoneyChanged -= UpdateValue;
        }

        private void UpdateValue(int amount) 
            => text.text = amount.ToString();
    }
}