using UnityEngine;
using Zenject;

namespace App.GamePausing
{
    public class GamePauseTrigger : MonoBehaviour
    {
        [Inject] private readonly GamePause _gamePause;
        
        private void OnEnable() 
            => _gamePause.SetPauseState(true);

        private void OnDisable() 
            => _gamePause.SetPauseState(false);
    }
}