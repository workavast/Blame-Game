using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private int gameplaySceneIndex;

        private void Awake()
        {
            startGameBtn.onClick.AddListener(StartGame);
            quitBtn.onClick.AddListener(Quit);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(gameplaySceneIndex);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}