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
        [SerializeField] private GameObject hide;
        [SerializeField] private GameObject fill;
        [SerializeField] private UnlockConfig unlockConfig;
        [SerializeField] private Button button;
        
        public UnlockState State { get; private set; }

        public event Action<UnlockConfig> OnClick;
        public event Action OnStateChanged; 

        private void Awake()
        {
            icon.sprite = unlockConfig.Perk.Icon;
            button.onClick.AddListener(Clicked);
        }

        private void OnDestroy() 
        {
            button.onClick.RemoveListener(Clicked);
        }
        
        public void SetState(UnlockState unlockState)
        {
            switch (unlockState)
            {
                case UnlockState.UnAvailable:
                    hide.SetActive(true);
                    fill.SetActive(false);
                    button.interactable = true;
                    break;
                case UnlockState.Available:
                    hide.SetActive(false);
                    fill.SetActive(false);
                    button.interactable = true;
                    break;
                case UnlockState.Unlocked:
                    hide.SetActive(false);
                    fill.SetActive(true);
                    button.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unlockState), unlockState, null);
            }
            State = unlockState;
            OnStateChanged?.Invoke();
        }
        
        public PerkConfig GetPerkDataConfig() 
            => unlockConfig.Perk;
        
        public UnlockConfig GetUnlockConfig() 
            => unlockConfig;
        
        private void Clicked() 
            => OnClick?.Invoke(unlockConfig);
    }
}