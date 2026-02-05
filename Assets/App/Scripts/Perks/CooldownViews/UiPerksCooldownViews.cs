using System.Collections.Generic;
using App.Utils;
using UnityEngine;

namespace App.Perks.CooldownViews
{
    public class UiPerksCooldownViews : MonoBehaviour
    {
        [SerializeField] private UiCooldownViewCell viewPrefab;
        [SerializeField] private RectTransform parent;

        private readonly List<UiCooldownViewCell> _views = new(2);
        
        private void Awake() 
            => parent.DestroyChildren();

        public UiCooldownViewCell CreateView()
        {
            var view =Instantiate(viewPrefab, parent);
            _views.Add(view);
            UpdateOrders();
            
            return view;
        }

        private void UpdateOrders()
        {
            if (_views.Count == 1)
            {
                _views[0].UpdateOrder(UiCooldownViewCellOrder.First);
                return;                
            }
            
            for (var i = 1; i < _views.Count-1; i++)
                _views[i].UpdateOrder(UiCooldownViewCellOrder.Middle);
            _views[^1].UpdateOrder(UiCooldownViewCellOrder.Last);
        }
    }
}