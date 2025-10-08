using UnityEngine;
using Zenject;

namespace App.PlayerProviding
{
    public class PlayerProviderUpdater : MonoBehaviour
    {
        [Inject] private readonly PlayerProvider _playerProvider;

        private void Update()
        {
            _playerProvider.UpdateHealth();
        }
    }
}