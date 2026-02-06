using App.UI;
using App.Unlocks.UI;
using UnityEngine;

namespace App.Unlocks
{
    public class UnlockScreen : DefaultScreen
    {
        [SerializeField] private UnlocksWindow unlocksWindow;
        [SerializeField] private LinesDrawer linesDrawer;
        
        public override void Initialize()
        {
            base.Initialize();
            
            unlocksWindow.Initialize();
            linesDrawer.Initialize();
        }
    }
}