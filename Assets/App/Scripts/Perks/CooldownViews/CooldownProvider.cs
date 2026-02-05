using App.Ecs.Attack.Cooldown;
using UnityEngine;
using Zenject;

namespace App.Perks.CooldownViews
{
    public class CooldownProvider : MonoBehaviour
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private AttackCooldownView attackCooldownView;
        [SerializeField, Range(0f, 1f)] private float initialValue = 0f;
        
        private UiPerksCooldownViews _uiPerksCooldownViews;
        private UiCooldownViewCell _uiView;
        
        [Inject]
        public void Construct(UiPerksCooldownViews uiPerksCooldownViews)
        {
            _uiPerksCooldownViews = uiPerksCooldownViews;
        }
        
        private void Start()
        {
            if (attackCooldownView != null)
                attackCooldownView.OnCooldownPercentageUpdate += UpdateView;
            
            _uiView = _uiPerksCooldownViews.CreateView();
            _uiView.SetIcon(icon);
            UpdateView(initialValue);
        }

        private void UpdateView(float percentage)
        {
            _uiView.UpdateView(percentage);
        }
    }
}