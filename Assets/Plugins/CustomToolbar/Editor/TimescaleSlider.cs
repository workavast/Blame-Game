using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Avastrad.Editor.Toolbar
{
    public static class TimescaleSlider
    {
        private const string ToolbarElementName = "Custom/Timescale-Slider";
        private const float MinTimeScale = 0f;
        private const float MaxTimeScale = 10f;
        private static float _displayedTimeScale = Time.timeScale;

        static TimescaleSlider()
        {
            EditorApplication.update += RefreshIfTimeScaleChanged;
        }

        [MainToolbarElement(ToolbarElementName, defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement TimeSlider()
        {
            _displayedTimeScale = Time.timeScale;

            var content = new MainToolbarContent("Time Scale", "Time Scale");
            var slider = new MainToolbarSlider(content, Time.timeScale, MinTimeScale, MaxTimeScale, OnSliderValueChanged);

            slider.populateContextMenu = (menu) =>
            {
                menu.AppendAction("Reset", _ =>
                {
                    Time.timeScale = 1f;
                    _displayedTimeScale = Time.timeScale;
                    MainToolbar.Refresh(ToolbarElementName);
                });
            };

            return slider;
        }

        private static void OnSliderValueChanged(float newValue)
        {
            Time.timeScale = newValue;
            _displayedTimeScale = Time.timeScale;
        }

        private static void RefreshIfTimeScaleChanged()
        {
            if (Mathf.Approximately(Time.timeScale, _displayedTimeScale))
                return;

            _displayedTimeScale = Time.timeScale;
            MainToolbar.Refresh(ToolbarElementName);
        }
    }
}
