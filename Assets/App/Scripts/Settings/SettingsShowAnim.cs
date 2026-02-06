using System;
using App.UI;
using App.UI.ShowAnims;
using UnityEngine;
using UnityEngine.UI;

namespace App.Settings
{
    [Serializable]
    public class SettingsShowAnim : ScaleShowAnim
    {
        [SerializeField] private Scrollbar scrollbar;

        public override void Play()
        {
            PlayScale(content, () => { scrollbar.value = 1; });

            if (showDuration <= 0)
                scrollbar?.SetValueWithoutNotify(1);
        }
    }
}