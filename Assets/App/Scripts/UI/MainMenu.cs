using App.ScenesReferencing;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private SceneReference gameplaySceneRef;

        [Inject] private readonly ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            startGameBtn.onClick.AddListener(StartGame);
            quitBtn.onClick.AddListener(Quit);
        }

        private void StartGame()
        {
            _sceneLoader.LoadScene(gameplaySceneRef.SceneIndex);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}