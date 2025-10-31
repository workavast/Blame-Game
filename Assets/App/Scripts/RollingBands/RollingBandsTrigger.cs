using UnityEngine;
using Zenject;

namespace App.RollingBands
{
    public class RollingBandsTrigger : MonoBehaviour
    {
        [Inject] private readonly RollingBandsToggler _rollingBandsToggler;
        
        private void OnEnable()
        {
            _rollingBandsToggler.SetVisibilityState(true);
        }

        private void OnDisable()
        {
            _rollingBandsToggler.SetVisibilityState(false);
        }
    }
}