using App.GamePausing;
using App.PlayerProviding;
using UnityEngine;
using Zenject;

namespace App.PlayerDeath
{
    public class DeathChecker : MonoBehaviour
    {
        [SerializeField] private PlayerDeadUi playerDeadUi;

        [Inject] private readonly PlayerProvider _playerProvider;
        [Inject] private readonly GamePause _gamePause;

        private void Awake()
        {
            _playerProvider.OnPlayerDied += OverGame;
            playerDeadUi.Hide();
        }

        private void OverGame()
        {
            playerDeadUi.Show();
            _gamePause.SetPauseState(true);
        }
    }
}