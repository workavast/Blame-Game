using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class QuitBtn : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private void Awake()
        {
            btn.onClick.AddListener(Quit);
        }
        
        private static void Quit()
        {
            Application.Quit();
        }
    }
}