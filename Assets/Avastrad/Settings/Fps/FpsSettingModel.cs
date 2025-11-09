using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Fps
{
    public class FpsSettingModel : ISettingModel
    {
        public int SelectedOptionIndex { get; private set; }

        public int Priority => _config.Priority;
        public IReadOnlyList<int> FpsOptions => _config.FpsOptions;
        public int DefaultOptionIndex => _config.DefaultOptionIndex;
        public int SelectedOption => _config.FpsOptions[SelectedOptionIndex];

        private readonly FpsConfig _config;
        
        public FpsSettingModel(FpsConfig config)
        {
            _config = config;
            SelectedOptionIndex = DefaultOptionIndex;
        }
        
        public void Set(int optionIndex) 
            => SelectedOptionIndex = optionIndex;

        public void Apply() 
            => Application.targetFrameRate = SelectedOption;

        public Type GetStateType() 
            => typeof(SettingState);

        public ISettingState GetState() 
            => new SettingState(this);

        public void LoadState(ISettingState genericState)
        {
            var state = (SettingState)genericState;
            SelectedOptionIndex = state.SelectedOptionIndex;
        }

        private struct SettingState : ISettingState
        {
            public int SelectedOptionIndex { get; set; }

            public SettingState(FpsSettingModel model)
            {
                SelectedOptionIndex = model.SelectedOptionIndex;
            }
        }
    }
}