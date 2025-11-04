using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Settings
{
    public class CloseWarningWindow : MonoBehaviour
    {
        [SerializeField] private Button saveBtn;
        [SerializeField] private Button cancelBtn;

        /// <summary>
        /// return bool if close with save, otherwise false
        /// </summary>
        public event Action<bool> OnClose;
        
        private void Awake()
        {
            saveBtn.onClick.AddListener(Save);
            cancelBtn.onClick.AddListener(Cancel);
        }

        private void Save() 
            => OnClose?.Invoke(true);

        private void Cancel() 
            => OnClose?.Invoke(false);
    }
}