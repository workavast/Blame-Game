using System.Collections.Generic;
using UnityEngine;

namespace App.EnemiesCountScaling.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemiesCountScalersConfig), menuName = "App/" + nameof(EnemiesCountScalersConfig))]
    public class EnemiesCountScalersConfig : ScriptableObject
    {
        [SerializeField] private List<EnemiesCountScalerConfig> configs;

        public IReadOnlyList<EnemiesCountScalerConfig> Configs => configs;
    }
}