using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary.UI
{
    public class BestiaryCloseBtn : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private BestiaryManager bestiaryManager;

        
        private void Awake() 
            => btn.onClick.AddListener(Disable);

        private void Disable()
            => bestiaryManager.Close();
    }
}