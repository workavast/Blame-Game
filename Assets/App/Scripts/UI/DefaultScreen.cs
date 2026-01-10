using App.Utils.Polymorphism;
using Avastrad.UI.UiSystem;
using UnityEngine;

namespace App.UI
{
    public class DefaultScreen : ScreenBase
    {
        [SerializeField, SerializeReference, Polymorphic] private ShowAnim showAnimation;
        
        protected override void Show(string[] args = null)
        {
            showAnimation?.Play(transform);
            base.Show(args);
        }
    }
}