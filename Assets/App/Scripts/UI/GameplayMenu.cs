using App.GamePausing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class GameplayMenu : MonoBehaviour
    {
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private Button quitBtn;

        [Inject] private readonly GamePause _gamePause;
        
        private void Awake()
        {
            continueBtn.onClick.AddListener(ContinueGame);
            backInMenuBtn.onClick.AddListener(BackInMenu);
            quitBtn.onClick.AddListener(QuitGame);
        }

        public void Open()
        {
            _gamePause.SetPauseState(true);
        }
        
        private void ContinueGame()
        {
            _gamePause.SetPauseState(false);
        }

        private void BackInMenu()
        {
            
        }

        private void QuitGame()
        {
            
        }
    }
}