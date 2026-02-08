using App.UI;
using App.Unlocks.UI;
using UnityEngine;

namespace App.Unlocks
{
    public class UnlockScreen : ScreenWithAnims
    {
        [SerializeField] private UnlocksWindow unlocksWindow;
        [SerializeField] private LinesDrawer linesDrawer;
        [SerializeField] private Transform showAnimationTransform;
        
        public override void Initialize()
        {
            base.Initialize();
            
            unlocksWindow.Initialize();
            linesDrawer.Initialize();
        }
    }
}