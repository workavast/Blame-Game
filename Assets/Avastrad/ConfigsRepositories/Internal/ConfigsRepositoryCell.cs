using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.ConfigsRepositories
{
    /// <remarks>
    /// fields used by reflection in custom property drawer, so dont rename them [<see cref="EditorConfigsRepositoryCellDrawer"/>]
    /// </remarks>
    [Serializable]
    internal struct ConfigsRepositoryCell<TConfig>
    {
        [SerializeField] private bool autoFill;
        [SerializeField] public List<TConfig> configs;

        public bool AutoFill => autoFill;
        public List<TConfig> Configs => configs;

        public ConfigsRepositoryCell(bool autoFill)
        {
            this.autoFill = autoFill;
            configs = new List<TConfig>();
        }
    }
}