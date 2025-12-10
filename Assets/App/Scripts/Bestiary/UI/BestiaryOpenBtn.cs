using UnityEngine;
using UnityEngine.UI;

namespace App.Bestiary.UI
{
    public class BestiaryOpenBtn : MonoBehaviour
    {
        [SerializeField] private BestiaryHolder bestiaryHolder;
        [SerializeField] private Button btn;

        private void Awake()
        {
            btn.onClick.AddListener(bestiaryHolder.Open);
        }
    }
}