using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.LevelManagement.ExpManagement
{
    public class PlayerExpUiView : MonoBehaviour
    {
        [SerializeField] private Slider expBarView;
        [SerializeField] private TMP_Text expTxtView;

        [Inject] private readonly IExpStorageRO _expStorage;
        
        private void Update()
        {
            var currentFillPercentage = _expStorage.FillTargetPercentage;
            if (!Mathf.Approximately(expBarView.value, currentFillPercentage))
            {
                expBarView.value = currentFillPercentage;
                expTxtView.text = $"{_expStorage.ExpAmount}/{_expStorage.ExpTarget}";
            }
        }
    }
}