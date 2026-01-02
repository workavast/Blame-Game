using System.Collections.Generic;
using App.Perks.PerksManagement;
using App.UI.Popup;
using UnityEngine;

namespace App.Perks.UI.ActivePerksView
{
    public class ActivePerksViewGrid : MonoBehaviour
    {
        [SerializeField] private PopupController popupController;
        [SerializeField] private ActivePerkViewCell perkViewCellPrefab;
        [SerializeField] private Transform contentContainer;
        
        private readonly List<ActivePerkViewCell> _activePerkViews = new();
        
        public void UpdateView(PerksStorage perksStorage)
        {
            for (var i = _activePerkViews.Count; i < perksStorage.ActivatedPerks.Count; i++)
            {
                var perkView = Instantiate(perkViewCellPrefab, contentContainer);
                _activePerkViews.Add(perkView);

                var popupActivator = perkView.GetComponent<PerkViewPopupActivator>();
                popupActivator?.Initialize(popupController);
            }
            
            for (var i = 0; i < _activePerkViews.Count; i++)
            {
                if (i < perksStorage.ActivatedPerks.Count)
                {
                    _activePerkViews[i].gameObject.SetActive(true);
                    _activePerkViews[i].SetPerk(perksStorage.ActivatedPerks[i]);
                }
                else
                {
                    _activePerkViews[i].gameObject.SetActive(false);
                }
            }
        }
    }
}