using App.EscProviding;
using App.GamePausing;
using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class GameplayMenu : MonoBehaviour, IEscListener
    {
        [SerializeField] private SceneReference mainMenuRef;
        [Space]
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private Button quitBtn;

        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly EscProvider _escProvider;
        [Inject] private readonly GamePause _gamePause;
        
        private void Awake()
        {
            continueBtn.onClick.AddListener(ContinueGame);
            backInMenuBtn.onClick.AddListener(BackInMenu);
            quitBtn.onClick.AddListener(QuitGame);
            _escProvider.Sub(this);
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (gameObject.activeSelf)
                _gamePause.SetPauseState(false);

            _escProvider.UnSub(this);
        }

        public void OnEscPressed()
        {
            _gamePause.SetPauseState(!gameObject.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
        }
        
        private void ContinueGame()
        {
            OnEscPressed();
        }

        private void BackInMenu()
        {
            _sceneLoader.LoadScene(mainMenuRef.SceneIndex);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}