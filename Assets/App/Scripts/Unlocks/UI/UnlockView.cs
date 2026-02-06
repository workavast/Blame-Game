using System;
using App.Perks.Configs;
using App.Unlocks.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace App.Unlocks.UI
{
    public class UnlockView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image hide;
        [SerializeField] private UnlockConfig unlockConfig;
        [SerializeField] private Button button;

        public event Action<UnlockConfig> OnClick;

        private void Awake()
        {
            icon.sprite = unlockConfig.Perk.Icon;
            button.onClick.AddListener(Clicked);
        }

        public void SetState(UnlockState unlockState)
        {
            switch (unlockState)
            {
                case UnlockState.UnAvailable:
                    hide.gameObject.SetActive(true);
                    button.interactable = true;
                    break;
                case UnlockState.Available:
                    hide.gameObject.SetActive(false);
                    button.interactable = true;
                    break;
                case UnlockState.Unlocked:
                    hide.gameObject.SetActive(false);
                    button.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unlockState), unlockState, null);
            }
        }
        
        private void OnDestroy() 
        {
            button.onClick.RemoveListener(Clicked);
        }
        
        public PerkConfig GetPerkDataConfig() 
            => unlockConfig.Perk;
        
        public UnlockConfig GetUnlockConfig() 
            => unlockConfig;
        
        private void Clicked() 
            => OnClick?.Invoke(unlockConfig);
    }
}