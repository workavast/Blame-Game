using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Fps
{
    [Serializable]
    public class FpsSettingsModel : ISettingsModel
    {
        [field: SerializeField] public int SelectedOptionIndex { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<int> FpsOptions => _config.FpsOptions;
        public int DefaultOptionIndex => _config.DefaultOptionIndex;
        public int SelectedOption => _config.FpsOptions[SelectedOptionIndex];

        private readonly FpsConfig _config;
        
        public FpsSettingsModel(FpsConfig config)
        {
            _config = config;
            SelectedOptionIndex = DefaultOptionIndex;
        }
        
        public void Load(FpsSettingsModel model) 
            => SelectedOptionIndex = model.SelectedOptionIndex;

        public void Set(int optionIndex) 
            => SelectedOptionIndex = optionIndex;

        public void Apply() 
            => Application.targetFrameRate = SelectedOption;
    }
}