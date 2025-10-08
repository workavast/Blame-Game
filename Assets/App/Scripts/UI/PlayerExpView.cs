using App.ExpManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    public class PlayerExpView : MonoBehaviour
    {
        [SerializeField] private Slider expBarView;
        [SerializeField] private TMP_Text expTxtView;

        [Inject] private readonly ExpManager _expManager;
        
        private void Update()
        {
            var currentFillPercentage = _expManager.FillPercentage;
            if (!Mathf.Approximately(expBarView.value, currentFillPercentage))
            {
                expBarView.value = currentFillPercentage;
                expTxtView.text = $"{_expManager.ExpAmount}/{_expManager.ExpTarget}";
            }
        }
    }
}