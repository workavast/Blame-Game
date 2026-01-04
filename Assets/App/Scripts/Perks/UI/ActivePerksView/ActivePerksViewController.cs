using App.Perks.PerksManagement;
using UnityEngine;
using Zenject;

namespace App.Perks.UI.ActivePerksView
{
    public class ActivePerksViewController : MonoBehaviour
    {
        [SerializeField] private ActivePerksViewGrid activePerksViewGrid;
        
        private PerksStorage _perksStorage;
        
        [Inject]
        private void Construct(PerksStorage perksStorage)
        {
            _perksStorage = perksStorage;
            _perksStorage.OnActivePerksChanged += UpdateView;
        }
        
        private void Start()
        {
            UpdateView();
        }
        
        private void UpdateView()
        {
            activePerksViewGrid.UpdateView(_perksStorage);
        }
        
        private void OnDestroy()
        {
            if (_perksStorage != null)
                _perksStorage.OnActivePerksChanged -= UpdateView;
        }
    }
}