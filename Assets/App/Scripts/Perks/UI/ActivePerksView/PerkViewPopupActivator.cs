using App.UI.Popup;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Perks.UI.ActivePerksView
{
    [RequireComponent(typeof(RectTransform))]
    public class PerkViewPopupActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ActivePerkViewCell activePerkViewCell;

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
            var config = activePerkViewCell.GetPerkDataConfig();
            _popupController.Show(popupPosition, config.GetTitleStr(), config.GetDescriptionStr());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _popupController.Hide();
        }
    }
}