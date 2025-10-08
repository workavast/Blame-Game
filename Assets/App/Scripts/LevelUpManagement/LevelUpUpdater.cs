using App.ExpManagement;
using UnityEngine;
using Zenject;

namespace App.LevelUpManagement
{
    public class LevelUpUpdater : MonoBehaviour
    {
        [Inject] private readonly LevelUpManager _levelUpManager;
        [Inject] private readonly ExpManager _expManager;

        private void Update()
        {
            if (_expManager.IsReachExpLimit())
            {
                _expManager.IncreaseExpLimit();
                _levelUpManager.LevelUp();
            }
        }
    }
}