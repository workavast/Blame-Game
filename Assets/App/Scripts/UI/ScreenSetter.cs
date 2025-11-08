using App.TypeReferencing;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class ScreenSetter : MonoBehaviour
    {
        [SerializeField] private TypeReference<ScreenBase> screen;
        
        [Inject] private readonly ScreensController _screensController;

        private void Awake() 
            => GetComponent<Button>().onClick.AddListener(()=>_screensController.SetScreen(screen.Type));
    }
}