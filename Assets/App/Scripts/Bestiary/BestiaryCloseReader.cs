using System;
using App.EscProviding;
using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary
{
    public class BestiaryCloseReader : MonoBehaviour, IEscListener
    {
        [SerializeField] private Button closeBestiaryBtn;
        
        private EscProvider _escProvider;

        public event Action OnCloseRequested;
        
        public void Initialize(EscProvider escProvider)
        {
            _escProvider = escProvider;
        }

        private void OnEnable()
        {
            _escProvider.Sub(this);
            closeBestiaryBtn.onClick.AddListener(RequestClose);
        }

        private void OnDisable()
        {
            _escProvider.UnSub(this);
            closeBestiaryBtn.onClick.RemoveListener(RequestClose);
        }

        public void OnEscPressed() 
            => RequestClose();

        private void RequestClose() 
            => OnCloseRequested?.Invoke();
    }
}