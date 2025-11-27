using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.LevelManagement.ExpManagement
{
    public class PlayerExpUiSliderView : MonoBehaviour
    {
        [SerializeField] private Slider expBarView;

        [Inject] private readonly IExpStorageRO _expStorage;
        
        private void Update()
        {
            var currentFillPercentage = _expStorage.FillTargetPercentage;
            if (!Mathf.Approximately(expBarView.value, currentFillPercentage)) 
                expBarView.value = currentFillPercentage;
        }
    }
}