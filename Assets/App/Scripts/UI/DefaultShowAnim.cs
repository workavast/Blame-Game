using System;
using UnityEngine;

namespace App.UI
{
    [Serializable]
    public class DefaultShowAnim : ShowAnim
    {
        public override void Play(Transform transform) 
            => PlayDefault(transform);
    }
}