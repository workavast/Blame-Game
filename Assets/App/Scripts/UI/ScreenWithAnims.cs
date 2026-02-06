using App.Utils.Polymorphism;
using Avastrad.UI.UiSystem;
using App.UI.ShowAnims;
using UnityEngine;

namespace App.UI
{
    public class ScreenWithAnims : ScreenBase
    {
        [SerializeField, SerializeReference, Polymorphic] protected ShowAnim[] showAnimations;

        protected override void Show(string[] args = null)
        {
            if (showAnimations != null)
                foreach (var anim in showAnimations)
                    anim?.Play();
            
            base.Show(args);
        }
    }
}