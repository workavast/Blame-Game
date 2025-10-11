using UnityEngine;
using Zenject;

namespace App.EnemiesCountScaling
{
    public class EnemiesCountScalersUpdater : MonoBehaviour
    {
        [Inject] private readonly EnemiesCountScalersHolder _enemiesCountScalerHolder;

        private void FixedUpdate()
        {
            _enemiesCountScalerHolder.UpdateScalers();
        }
    }
}