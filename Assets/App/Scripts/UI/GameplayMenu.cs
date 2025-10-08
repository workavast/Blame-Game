using System;
using App.EcsPausing;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class GameplayMenu : MonoBehaviour
    {
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private Button quitBtn;

        private EcsPause _ecsPause;
        
        private void Awake()
        {
            continueBtn.onClick.AddListener(ContinueGame);
            backInMenuBtn.onClick.AddListener(BackInMenu);
            quitBtn.onClick.AddListener(QuitGame);
        }

        private void Start()
        {
            _ecsPause = ServicesBridge.Get<EcsPause>();
        }

        public void Open()
        {
            _ecsPause.SetPauseState(true);
        }
        
        private void ContinueGame()
        {
            _ecsPause.SetPauseState(false);
        }

        private void BackInMenu()
        {
            
        }

        private void QuitGame()
        {
            
        }
    }
}