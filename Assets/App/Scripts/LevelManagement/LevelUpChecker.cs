using App.LevelManagement.ExpManagement;
using UnityEngine;
using Zenject;

namespace App.LevelManagement
{
    public class LevelUpChecker : MonoBehaviour
    {
        [Inject] private readonly LevelStorage _levelStorage;
        [Inject] private readonly ExpStorage _expStorage;

        private void Update()
        {
            if (_expStorage.IsReachExpTarget())
            {
                _levelStorage.LevelUp();
                _expStorage.IncreaseExpTarget();
            }
        }
    }
}