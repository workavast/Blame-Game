using TMPro;
using UnityEngine;
using Zenject;

namespace App.LevelManagement
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text field;

        [Inject] private readonly ILevelStorageRO _levelStorage;

        private void OnEnable()
            => _levelStorage.OnLevelUp += UpdateValue;

        private void OnDisable()
            => _levelStorage.OnLevelUp -= UpdateValue;

        private void UpdateValue()
            => field.text = _levelStorage.Level.ToString();
    }
}