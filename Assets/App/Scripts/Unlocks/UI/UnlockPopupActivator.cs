using App.ResourcesSystem.ResourcesConfigs;
using App.UI.Popup;
using App.Unlocks.Storage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using Zenject;

namespace App.Unlocks.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UnlockPopupActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnlockView unlockView;
        [SerializeField] private LocalizedString costTitle;
        
        [Inject] private readonly ResourcesConfigsStorage _resourcesConfigsStorage;

        private PopupController _popupController;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(PopupController popupController)
        {
            _popupController = popupController;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Show();
            unlockView.OnStateChanged += Show;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _popupController.Hide();
            unlockView.OnStateChanged -= Show;
        }
        
        private void Show()
        {
            var popupPosition = _rectTransform.position;
            var config = unlockView.GetPerkDataConfig();
            var description = BuildDescription(unlockView);
            
            _popupController.Show(popupPosition, config.GetTitle(), description);
        }
        
        private string BuildDescription(UnlockView unlockView)
        {
            var config = unlockView.GetUnlockConfig();
            var mainDescription = config.Perk.GetDescription();
            var costs = config.Cost;

            string description;
            if (unlockView.State == UnlockState.Unlocked) 
                description = mainDescription;
            else
            {
                if (costs.Resources.Count == 0)
                {
                    description = mainDescription;
                }
                else
                {
                    var costsStr = string.Empty;
                    foreach (var cost in costs.Resources)
                    {
                        var resourceConfig = _resourcesConfigsStorage.GetConfig(cost.Key);
                        costsStr += $"\n<sprite={resourceConfig.SpriteAssetIndex}> {cost.Value}";
                    }
                
                    description = $"{mainDescription}" +
                                  $"\n" +
                                  $"\n{costTitle.GetLocalizedString()}" +
                                  $"{costsStr}";                    
                }
            }
            
            return description;
        }
    }
}