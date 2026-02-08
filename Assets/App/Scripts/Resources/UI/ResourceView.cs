using App.Resources.Cells;
using App.Resources.ResourcesConfigs;
using Avastrad.Libs.CheckOnNullLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Resources.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text text;
        
        private IReadOnlyResourceCell _resourceCell;

        public void Initialize(IReadOnlyResourceCell resourceCell, ResourceConfig config)
        {
            _resourceCell = resourceCell;
            icon.sprite = config.Icon;
        }

        public void ManualOnEnable()
        {
            _resourceCell.OnChanged += UpdateValue;
            UpdateValue(_resourceCell.Amount);
        }

        public void ManualOnDisable()
        {
            if (_resourceCell.IsAnyNull())
                return;
            
            _resourceCell.OnChanged -= UpdateValue;
        }

        private void OnDestroy() 
            => ManualOnDisable();

        private void UpdateValue(int amount) 
            => text.text = amount.ToString();
    }
}