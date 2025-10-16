using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.GameEndDetection
{
    public class GameLooseUi : MonoBehaviour
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button backInMenuBtn;
        [SerializeField] private SceneReference gameplaySceneRef;
        [SerializeField] private SceneReference mainMenuSceneRef;

        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            restartBtn?.onClick.AddListener(Restart);
            backInMenuBtn?.onClick.AddListener(LoadMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Restart()
        {
            _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex);
        }
        
        private void LoadMenu()
        {
            _sceneLoader.LoadScene(mainMenuSceneRef.SceneIndex);
        }
    }
}