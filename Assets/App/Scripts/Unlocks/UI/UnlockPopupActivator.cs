using App.UI.Popup;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Unlocks.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UnlockPopupActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnlockView unlockView;

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
            var popupPosition = _rectTransform.position;
            var config = unlockView.GetPerkDataConfig();
            _popupController.Show(popupPosition, config.GetTitle(), config.GetDescription());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _popupController.Hide();
        }
    }
}