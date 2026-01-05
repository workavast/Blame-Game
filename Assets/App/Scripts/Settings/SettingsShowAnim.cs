using System;
using App.UI;
using UnityEngine;
using UnityEngine.UI;

namespace App.Settings
{
    [Serializable]
    public class SettingsShowAnim : ShowAnim
    {
        [SerializeField] private Scrollbar scrollbar;

        public override void Play(Transform transform)
        {
            PlayDefault(transform, () => { scrollbar.value = 1; });

            if (showDuration <= 0)
                scrollbar?.SetValueWithoutNotify(1);
        }
    }
}