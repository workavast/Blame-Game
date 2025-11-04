using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    public abstract class ScreenBase : MonoBehaviour
    {
        public virtual void Initialize() {}
        
        public void SetActive(bool isActive, string[] args = null)
        {
            if (isActive)
                Show(args);
            else
                Hide(args);
        }
        
        protected virtual void Show(string[] args = null)
            => gameObject.SetActive(true);

        protected virtual void Hide(string[] args = null) 
            => HideInstantly();
        
        protected virtual void HideInstantly()
            => gameObject.SetActive(false);
    }
}