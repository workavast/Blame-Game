using TMPro;
using UnityEngine;
using Zenject;

namespace App.LevelManagement.ExpManagement
{
    public class PlayerExpUiTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text expTxtView;

        [Inject] private readonly IExpStorageRO _expStorage;

        private void Awake()
        {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            gameObject.SetActive(false);
#endif
        }

        private void Update()
        {
            expTxtView.text = $"{_expStorage.ExpAmount}/{_expStorage.ExpTarget}";
        }
    }
}