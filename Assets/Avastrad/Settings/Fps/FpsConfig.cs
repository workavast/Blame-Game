using System.Collections.Generic;
using UnityEngine;

namespace Avastrad.Settings.Fps
{
    [CreateAssetMenu(fileName = nameof(FpsConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(FpsConfig))]
    public class FpsConfig : SettingConfig
    {
        [Space]
        [SerializeField] private List<int> fpsOptions = new() { 30, 60, 90, 120, 144, 240, 360 };
        [SerializeField, Min(0)] private int defaultOptionIndex;

        public IReadOnlyList<int> FpsOptions => fpsOptions;
        public int DefaultOptionIndex => defaultOptionIndex;
        public int DefaultOption => fpsOptions[defaultOptionIndex];

        /// <param name="fps"> 30, 60 etc.</param>
        /// <returns>index if it exist, else default index</returns>
        public int IndexOf(int fps)
        {
            var optionIndex = fpsOptions.IndexOf(fps);
            if (optionIndex < 0)
            {
                Debug.LogError($"Cant find option: [{fps}]");
                optionIndex = DefaultOptionIndex;
            }

            return optionIndex;
        }
        
        public override ISettingModel CreateModel() 
            => new FpsSettingModel(this);
    }
}